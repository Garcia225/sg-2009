select	fac.num_factura as numFac,
		prov.razon_social as razonSocial,
		fac.total_factura as totalFac,
		fac.fecha as fecha,
		cond.contado as contado
from	PCCC_FACTURA as fac,
		PCCC_PROVEEDORES as prov,
		PCCC_CONDICION_DE_PAGO as cond,
		PCCC_ESTADO as est
where	prov.id_proveedor = fac.id_proveedor
and		prov.borrado = 'N'
and		cond.id_condicion_de_pago = fac.id_condicion_de_pago
and		fac.id_estado = est.id_estado
and		est.descripcion <> 'ANULADO'