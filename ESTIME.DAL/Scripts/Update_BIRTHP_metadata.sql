UPDATE eft
SET  eft.IsUniform = 1, eft.ColumnDelimiterId = cd.Id
from ESTIME.tl_EstimeFileType eft
cross join ESTIME.tl_ColumnDelimiter cd
WHERE eft.code = 'BIRTHP' and cd.Code = ',';

delete ivv
from ESTIME.tl_InputVariableValue ivv
inner join ESTIME.tl_InputVariable iv
on iv.id = ivv.InputVariableId
inner join ESTIME.tl_EstimeFileType eft
on eft.id = iv.EstimeFileTypeId
where eft.Code = 'BIRTHP';

delete ic
from ESTIME.tl_InputCoordinate ic
inner join ESTIME.tl_InputVariable iv
on iv.Id = ic.InputVariableId
inner join ESTIME.tl_EstimeFileType eft
on eft.id = iv.EstimeFileTypeId
where eft.Code = 'BIRTHP';

delete iv
from ESTIME.tl_InputVariable iv
inner join ESTIME.tl_variable v
on v.Id =iv.VariableId
inner join ESTIME.tl_EstimeFileType eft
on eft.id = iv.EstimeFileTypeId
where eft.Code = 'BIRTHP' and v.Code = 'SEX';

insert into ESTIME.tl_InputVariable
(EstimeFileTypeId, VariableId, DisplayOrder, isMandatory, isParameter)
select eft.Id, v.Id, null, 1, 0
from ESTIME.tl_EstimeFileType eft
cross join ESTIME.tl_Variable v
where eft.Code = 'BIRTHP' and v.Code in ('BirthYYYY', 'BirthMM')

insert into ESTIME.tl_InputCoordinate
(RecordNumber, InputVariableId, ColumnNumber, RowNumber)
select 1, iv.Id, iif(v.Code = 'BirthYYYY', 1, iif(v.Code = 'BirthMM', 2, 3)), NULL
from ESTIME.tl_InputVariable iv
inner join ESTIME.tl_EstimeFileType eft
on eft.Id = iv.EstimeFileTypeId and eft.Code = 'BIRTHP'
inner join ESTIME.tl_Variable v
on v.Id = iv.VariableId and v.Code in ('BirthYYYY', 'BirthMM', 'RECORDVALUE')