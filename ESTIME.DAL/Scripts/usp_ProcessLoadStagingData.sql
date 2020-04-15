USE [ESTIME_Dev]
GO

/****** Object:  StoredProcedure [ESTIME].[usp_ProccessLoadStagingData]    Script Date: 2020-04-15 1:02:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================================================================
-- Author:		chensha
-- Create date: 2020-04-09
-- Description:	Read the load staging data in td_LoadStaging table for a given LoadId and 
--				save the data in td_LoadData table
-- Assumptions: The data in the td_LoadStaging is in the same calendar year and of monthly data
-- Pre-conditions: 
-- Changes:
-- ================================================================================================

CREATE PROCEDURE [ESTIME].[usp_ProccessLoadStagingData] 
@LoadId AS BIGINT,
@RefPeriodId AS INT,
@SuccessCode AS INT= 0 OUTPUT, --0 is successful - all other values are failures - can be mutiple values
@ErrorExceptionMessage AS NVARCHAR(MAX)='' OUTPUT
AS


/**************************************************************************/
/*XACT_ABORT ON allows for more reliable error and transaction handling.  */
/*Almost all errors will cause an open transaction to be rolled back and  */
/*execution is aborted.                                                   */
/*NOCOUNT ON suppresses row counts that can degrade performance in an     */
/*application because of increased network traffic                        */
/**************************************************************************/
SET XACT_ABORT, NOCOUNT ON 

BEGIN TRY
	DECLARE @EstimeFileTypeId INT;
	DECLARE @EstimeFileTypeCode VARCHAR(10);

	SELECT @EstimeFileTypeId = l.EstimeFileTypeId,
			@EstimeFileTypeCode = eft.Code
	FROM ESTIME.td_Load l
	INNER JOIN ESTIME.tl_EstimeFileType eft
	ON eft.Id = l.EstimeFileTypeId
	WHERE l.Id = @LoadId;

	SET @SuccessCode = 0;

	DECLARE @RefPeriodCode VARCHAR(6);
	SELECT @RefPeriodCode = RTRIM(LTRIM(Code))
	FROM ESTIME.tc_RefPeriod
	WHERE Id = @RefPeriodId;

	DECLARE @YearCode VARCHAR(4) = SUBSTRING(@RefPeriodCode, 1, 4);

	DECLARE @IsUniform BIT;
	SELECT @IsUniform = @IsUniform
	FROM ESTIME.tl_EstimeFileType
	WHERE Id = @EstimeFileTypeId;

	DECLARE @ColDelimiter VARCHAR(10);

	SELECT @ColDelimiter = IIF(eft.ColumnDelimiterId IS NULL, '', cd.Code)
	FROM ESTIME.tl_EstimeFileType eft
	LEFT JOIN ESTIME.tl_ColumnDelimiter cd
	ON cd.Id = eft.ColumnDelimiterId
	WHERE eft.Id = @EstimeFileTypeId;

	--Get all the input varaibles for the Estime file type
	DECLARE @InputVar TABLE(InputVarId INT, VarPos INT, VarLenght INT, VarCode VARCHAR(50));
	INSERT INTO @InputVar
	(
		InputVarId, 
		VarPos, 
		VarLenght,
		VarCode
	)
	SELECT ic.InputVariableId, ic.ColumnNumber, v.MaxLength, v.Code
	FROM ESTIME.tl_InputCoordinate ic
	INNER JOIN ESTIME.tl_InputVariable iv
	ON iv.Id = ic.InputVariableId
	INNER JOIN ESTIME.tl_Variable v
	ON v.Id = iv.VariableId
	WHERE iv.isParameter = 0 AND iv.EstimeFileTypeId = @EstimeFileTypeId AND iv.isMandatory = 1;

	DECLARE @InputVarValue TABLE(RecId INT, InputVarId INT, VarCode VARCHAR(50), VarValue VARCHAR(500), RefPeriodCode VARCHAR(50));

	--read in data from the staging table
	IF (@ColDelimiter = '')
	BEGIN
		--with no delimiter
		INSERT INTO @InputVarValue
		(
			RecId,
			InputVarId,
			VarCode,
			VarValue
		)
		SELECT lt.RecordId, iv.InputVarId, iv.VarCode, SUBSTRING(lt.RecordValue, iv.VarPos, iv.VarLenght)
		FROM ESTIME.td_LoadStaging lt
		CROSS JOIN @InputVar iv
		WHERE lt.LoadId = @LoadId;
	END
	ELSE
	BEGIN
		--with delimiter
		INSERT INTO @InputVarValue
		(
			RecId,
			InputVarId,
			VarCode,
			VarValue
		)
		SELECT  rv.RecordId, iv.InputVarId, iv.VarCode, rv.VarValue
		FROM (SELECT lt.RecordId, Value AS VarValue, ROW_NUMBER() OVER (PARTITION BY lt.RecordId ORDER BY lt.RecordId) AS RecVarOrder
			FROM Estime.td_LoadStaging lt
			CROSS APPLY STRING_SPLIT(lt.RecordValue, @ColDelimiter)
			WHERE lt.LoadId = @LoadId) AS rv
		INNER JOIN @InputVar iv
		ON iv.VarPos = rv.RecVarOrder;
	END

	IF (EXISTS(SELECT * FROM @InputVarValue WHERE VarCode LIKE '%MM%' OR VarCode LIKE '%YYYY%'))
	BEGIN
		--retrieve the reference period info by finding Variables like '%MM%' and '%YYYY%'
	    UPDATE ivv
		SET ivv.RefPeriodCode = CASE WHEN ivv.VarCode LIKE '%MM%' OR ivv.VarCode LIKE '%YYYY%' THEN ivv.VarValue END
		FROM @InputVarValue ivv;

		UPDATE ivv1
		SET ivv1.RefPeriodCode = @YearCode + LTRIM(RTRIM(ivv2.RefPeriodCode))
		FROM @InputVarValue ivv1
		INNER JOIN @InputVarValue ivv2 
		ON ivv2.RecId = ivv1.RecId AND ivv2.VarCode LIKE '%MM%';
	END
	ELSE
    BEGIN
		--If there's no MM or YYYY Variables, then use the RefPeriodCode derived from input @RefPeriodId
		UPDATE @InputVarValue
		SET RefPeriodCode = @RefPeriodCode;
	END
    

	--delete the MM and YYYY variable values, they are converted to RefPeriodCode
	DELETE FROM @InputVarValue
	WHERE VarCode LIKE '%MM%' OR VarCode LIKE '%YYYY%';


	--delete the records that not in the reference year 
	DELETE FROM @InputVarValue
	WHERE SUBSTRING(RefPeriodCode, 1, 4) <> @YearCode;

	-- insert the RECORDVALUE variable
	INSERT INTO @InputVarValue
	(
		RecId,
		InputVarId,
		VarCode,
		VarValue,
		RefPeriodCode
	)
	SELECT DISTINCT ivv.RecId, iv2.Id, 'RECORDVALUE', '1', ivv.RefPeriodCode
	FROM @InputVarValue ivv
	CROSS JOIN (SELECT iv.Id FROM ESTIME.tl_InputVariable iv
				INNER JOIN ESTIME.tl_Variable v
				ON v.Id = iv.VariableId AND v.Code = 'RECORDVALUE'
				WHERE iv.EstimeFileTypeId = @EstimeFileTypeId) AS iv2;

	--Birth and Death data need to be aggregated by PROV, SEX, and AGE
	IF (@EstimeFileTypeCode = 'BIRTH' OR @EstimeFileTypeCode = 'DEATH')
	BEGIN
		--Birth data file need to aggregate before insert into the td_LoadData table
		DECLARE @ProvVarId INT, @SexVarId INT, @AgeVarId INT, @RecordValueVarId INT;

		SELECT @ProvVarId = iv.Id
		FROM ESTIME.tl_InputVariable iv
		INNER JOIN ESTIME.tl_Variable v
		ON v.Id = iv.VariableId AND v.Code = 'PROV'
		WHERE iv.EstimeFileTypeId = 1;
		                
		SELECT @SexVarId  = iv.Id
		FROM ESTIME.tl_InputVariable iv
		INNER JOIN ESTIME.tl_Variable v
		ON v.Id = iv.VariableId AND v.Code = 'SEX'
		WHERE iv.EstimeFileTypeId = 1;

		SELECT @AgeVarId = iv.Id
		FROM ESTIME.tl_InputVariable iv
		INNER JOIN ESTIME.tl_Variable v
		ON v.Id = iv.VariableId AND v.Code = 'Age'
		WHERE iv.EstimeFileTypeId = 1;

		SELECT @RecordValueVarId = iv.Id
		FROM ESTIME.tl_InputVariable iv
		INNER JOIN ESTIME.tl_Variable v
		ON v.Id = iv.VariableId AND v.Code = 'RECORDVALUE'
		WHERE iv.EstimeFileTypeId = 1;

		DECLARE @TempFat TABLE(RecordId INT, RefPeriodCode VARCHAR(10), ProvValue VARCHAR(10), SexValue VARCHAR(10), AgeValue VARCHAR(10), RecordValue VARCHAR(10))

		INSERT INTO @TempFat
		(
		    RecordId,
		    RefPeriodCode,
		    ProvValue,
		    SexValue,
		    AgeValue,
		    RecordValue
		)
		SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)), ivv.RefPeriodCode, ivvProv.VarValue, ivvSex.VarValue, ivvAge.VarValue, COUNT(DISTINCT ivv.RecId)
		FROM @InputVarValue ivv
		INNER JOIN (SELECT RecId, VarValue, RefPeriodCode FROM @InputVarValue WHERE InputVarId = @ProvVarId) AS ivvProv
		ON ivvProv.RecId = ivv.RecId AND ivvProv.RefPeriodCode = ivv.RefPeriodCode
		INNER JOIN (SELECT RecId, VarValue, RefPeriodCode FROM @InputVarValue WHERE InputVarId = @SexVarId) AS ivvSex
		ON ivvSex.RecId = ivv.RecId AND ivvSex.RefPeriodCode = ivv.RefPeriodCode
		INNER JOIN (SELECT RecId, VarValue, RefPeriodCode FROM @InputVarValue WHERE InputVarId = @AgeVarId) AS ivvAge
		ON ivvAge.RecId = ivv.RecId AND ivvAge.RefPeriodCode = ivv.RefPeriodCode
		GROUP BY ivv.RefPeriodCode, ivvProv.VarValue, ivvSex.VarValue, ivvAge.VarValue;

		INSERT INTO ESTIME.td_LoadData
		(
		    LoadId,
		    RecordNumber,
		    InputVariableId,
		    RefPeriodId,
		    VariableValue
		)
		SELECT inData.LoadId, inData.RecordId, inData.InpurtVariableId, rp.Id, inData.VariableValue
		FROM (SELECT @LoadId AS LoadId, tf.RecordId, @ProvVarId AS InpurtVariableId, tf.RefPeriodCode, tf.ProvValue AS VariableValue FROM @TempFat tf
				UNION
				SELECT @LoadId, tf.RecordId, @SexVarId, tf.RefPeriodCode, tf.SexValue FROM @TempFat tf
				UNION
				SELECT @LoadId, tf.RecordId, @AgeVarId, tf.RefPeriodCode, tf.AgeValue FROM @TempFat tf
				UNION
				SELECT @LoadId, tf.RecordId, @RecordValueVarId, tf.RefPeriodCode, tf.RecordValue FROM @TempFat tf) AS inData
		INNER JOIN ESTIME.tc_RefPeriod rp
		ON rp.Code = inData.RefPeriodCode;;
	END
	ELSE
    BEGIN
		--insert into the data table without any aggregation
		INSERT INTO ESTIME.td_LoadData
		(
			LoadId,
			RecordNumber,
			InputVariableId,
			RefPeriodId,
			VariableValue
		)
		SELECT @LoadId, ivv.RecId, ivv.InputVarId, rp.Id, ivv.VarValue
		FROM @InputVarValue ivv
		INNER JOIN ESTIME.tc_RefPeriod rp
		ON rp.Code = ivv.RefPeriodCode;
	END	

	
END TRY

BEGIN CATCH
	SET @SuccessCode=1
    SET @ErrorExceptionMessage = ERROR_PROCEDURE() + SPACE(5) + ERROR_MESSAGE()
END CATCH



GO


