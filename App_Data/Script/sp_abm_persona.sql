set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

/* Procedimiento para alta, baja y modificaciones de la tabla persona
   Se utiliza la variable @opcion para identificar el tipo de procedimiento
   1 = insertar
   2 = modificar
*/


create procedure [dbo].[sp_abm_persona](
			@opcion varchar(1),	--not null
			@num_doc int, --not null
			@tipo_doc varchar(20),
			@nombre varchar(50),--not null
			@apellido varchar(50) = '-',--valores default
			@direccion varchar(50) = '-',
			@email varchar(50)= '-',
			@nacionalidad	varchar(20),--not null
			@sexo char(1))
			as 
			-- INICIO --
			BEGIN
			
			  BEGIN TRY
				BEGIN TRANSACTION  
					if(@opcion = '1')/*insertar*/
						begin
								
								 insert into PCCC_PERSONA ( num_doc, --not null
																tipo_doc,
																nombre,--not null
																apellido,--valores default
																direccion,
																email,
																nacionalidad,--not null
																sexo)
														values( @num_doc, --not null
																@tipo_doc,
																@nombre,--not null
																@apellido,--valores default
																@direccion,
																@email,
																@nacionalidad,--not null
																@sexo)
										
										
						           
						end
						else if(@opcion = '2')/*editar*/
							begin
							
							update PCCC_PERSONA
								set num_doc = @num_doc, --not null
									tipo_doc = @tipo_doc,
									nombre = @nombre,--not null
									apellido = @apellido,--valores default
									direccion = @direccion,
									email = @email,
									nacionalidad = @nacionalidad,--not null
									sexo = @sexo
							  where num_doc = @num_doc
							end

				COMMIT TRANSACTION
		END TRY
		BEGIN CATCH
					ROLLBACK TRANSACTION -- O solo ROLLBACK
					PRINT 'Se ha producido un error!'
		END CATCH


end
