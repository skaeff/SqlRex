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
	object_id = @object_id