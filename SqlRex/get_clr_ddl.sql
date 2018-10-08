select
--	'[' + isnull(o.name, '') + '] at ' + isnull(a.name, '') + '.dll => ' + isnull(am.assembly_class, '') + '.[' + isnull(am.assembly_method, '') + ']' + char(13),
	'[' + isnull(o.name, '') + '] at ' o1, isnull(a.name, '') + '.dll => ' o2, isnull(am.assembly_class, '') + '.[' o3, isnull(am.assembly_method, '') + ']' + char(13) o4,
	isnull((select p.name + ' ' + t.name + '(' + case convert(varchar(255), p.max_length) when '-1' then 'max' else convert(varchar(255), p.max_length) end  + ')' + char(13) from sys.parameters p
		inner join sys.types t on p.user_type_id = t.user_type_id
		 where p.object_id = o.object_id for xml path(''), type), '').value('.', 'NVARCHAR(MAX)') o5
from sys.objects o
	inner join sys.assembly_modules am on o.object_id = am.object_id
	inner join sys.assemblies a on a.assembly_id = am.assembly_id
where o.object_id = object_id(@clr_name_ext)

