select
	OBJECT_DEFINITION(object_id) txt_part,
	o.[type],
	s.name + '.' + o.name full_name,
	o.type_desc,
	o.object_id id,
	o.name as name

from sys.objects o
	inner join sys.schemas s on s.schema_id = o.schema_id
where
	(OBJECT_DEFINITION(object_id) is not null 
		and type not in ('C', 'D')
		) or type = 'U'
order by s.name + '.' + o.name