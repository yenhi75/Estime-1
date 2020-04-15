USE [ESTIME_Dev]
GO

/****** Object:  StoredProcedure [ESTIME].[usp_BulkLoadTextFile]    Script Date: 2020-04-15 1:01:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================================================================
-- Author:		chensha
-- Create date: 2020-04-09
-- Description:	Use BULK INSERT to read text or csv file into td_LoadStaging table
-- Pre-conditions: file path has already been inserted in td_Load table
-- Changes:
-- ================================================================================================

CREATE PROCEDURE [ESTIME].[usp_BulkLoadTextFile] 
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
	DECLARE @FilePath VARCHAR(500);

	SELECT @FilePath = l.FilePath
	FROM ESTIME.td_Load l
	INNER JOIN ESTIME.tl_EstimeFileType eft
	ON eft.Id = l.EstimeFileTypeId
	WHERE l.Id = @LoadId;

	SET @SuccessCode = 0;

	--Use table variable to hold the data from file
	DECLARE @bulkSql nvarchar(2048);

	DECLARE @LoadTable TABLE(ColumnValue VARCHAR(max));
	SET @bulkSql = N'CREATE TABLE #tmp (line varchar(max));
					BULK INSERT #tmp' + ' FROM ' +''''+ @FilePath +''''+ ' WITH (ROWTERMINATOR = ''\n'', FIRSTROW = 1, CODEPAGE=''ACP'');
					SELECT * FROM #tmp';
	INSERT INTO @LoadTable
	EXEC(@bulkSql);

	--Assumption: the text file is always uniform data, needs to be saved into staging table
	INSERT INTO Estime.td_LoadStaging
	(
		LoadId,
		RecordId,
		RecordValue
	)
	SELECT @LoadId, ROW_NUMBER() OVER(ORDER BY (SELECT NULL)), ColumnValue
	FROM @LoadTable;
	
	EXEC ESTIME.usp_ProcessLoadStagingData @LoadId, @RefPeriodId, @SuccessCode, @ErrorExceptionMessage;

END TRY

BEGIN CATCH
	SET @SuccessCode=1
    SET @ErrorExceptionMessage = ERROR_PROCEDURE() + SPACE(5) + ERROR_MESSAGE()
END CATCH



GO


