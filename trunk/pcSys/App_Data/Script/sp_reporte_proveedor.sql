
/* Procedimiento para ordenar reportes por criterio
opciones:
- 0 - nombre
- 1 - apellido
- 2 - fecha de nacimiento
- 3 - localidad
- 4 - limite de credito
- 5 - codigo
*/

CREATE PROCEDURE [dbo].[sp_reporte_proveedor](@opcion int, @filtro varchar(50))
AS

			IF(@opcion = 0)
				BEGIN
					SELECT		id_proveedor,
								razon_social,
								apellido,
								num_doc,
								direccion,
								telefono
					FROM        PCCC_PROVEEDORES 
				    WHERE		UPPER(razon_social) LIKE ('%' + UPPER(@filtro) + '%') 
					AND			(borrado = 'N')
					ORDER BY	razon_social	
				END 
			ELSE IF (@opcion = 1)/*APELLIDO*/
				BEGIN
			
					SELECT		id_proveedor,
								razon_social,
								apellido,
								num_doc,
								direccion,
								telefono
					FROM        PCCC_PROVEEDORES 
				    WHERE		UPPER(apellido) LIKE ('%' + UPPER(@filtro) + '%') 
					AND			(borrado = 'N')
					ORDER BY	apellido
				END 
			ELSE IF (@opcion = 2)
				BEGIN
			SELECT				id_proveedor,
								razon_social,
								apellido,
								num_doc,
								direccion,
								telefono
					FROM        PCCC_PROVEEDORES 
				    WHERE		id_proveedor LIKE ('%' + id_proveedor + '%') 
					AND			(borrado = 'N')
					ORDER BY	apellido
				END
			
return