select	fac.num_factura as numFac,
		prov.razon_social as razonSocial,
		fac.total_factura as totalFac,
		fac.fecha as fecha
from	PCCC_FACTURA as fac,
		PCCC_PROVEEDORES as prov
where	prov.id_proveedor = fac.id_proveedor
and		prov.borrado = 'N'