SELECT 
 af.name,
 af.content 
FROM sys.assemblies a
INNER JOIN sys.assembly_files af ON a.assembly_id = af.assembly_id 