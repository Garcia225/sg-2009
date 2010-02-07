CREATE PROCEDURE [dbo].[sp_reporte_factura](@opcion int, @filtro int, @fecha_inicio datetime, @fecha_fin datetime)
AS
	IF ( @opcion = 1) 
	BEGIN
		SELECT DISTINCT fac.id_factura,
						fac.num_factura,
						prov.razon_social,
						fac.fecha,
						est.descripcion,
						fac.total
						FROM	PCCC_FACTURA as fac,
								PCCC_ESTADO as est,
								PCCC_PROVEEDOR as prov
						WHERE	prov.borrado = 'N'
						and		prov.id_proveedor = @filtro
						and		fac.fecha between @fecha_inicio and @fecha_fin
	END
	ELSE
	BEGIN
		SELECT DISTINCT fac.id_factura,
						fac.num_factura,
						prov.razon_social,
						fac.fecha,
						est.descripcion,
						fac.total
						FROM	PCCC_FACTURA as fac,
								PCCC_ESTADO as est,
								PCCC_PROVEEDOR as prov
						WHERE	prov.borrado = 'N'
						and		fac.fecha between @fecha_inicio and @fecha_fin
	END
RETURN
