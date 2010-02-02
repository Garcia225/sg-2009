using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;

public partial class Reportes_reporteProveedor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cargar();
    }
    protected void cargar()
    {
        ObjectDataSource odsProveedor = new ObjectDataSource("dsProveedorTableAdapters.spProveedoresTableAdapter", "GetData");
        ReportDataSource pdsProveedor = new ReportDataSource("dsProveedor_spProveedores", odsProveedor);
        rvProveedor.LocalReport.DataSources.Clear();
        rvProveedor.LocalReport.DataSources.Add(pdsProveedor);
        rvProveedor.LocalReport.ReportPath = "Reportes/Proveedor.rdlc";
        rvProveedor.LocalReport.Refresh();

    }
}
