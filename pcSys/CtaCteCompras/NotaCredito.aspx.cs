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

public partial class NotadeCredito : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    [System.Web.Services.WebMethod()]
    public static string facturasPendientes(int idProveedor)
    {
        // Pide los datos a la clase Factura y lo devuelve
        Factura factura = new Factura();
        return factura.obtenerFactura(idProveedor);
    }
    [System.Web.Services.WebMethod()]
    public static string guardar(string idNota,string num_nota,string fecha,string total_credito,string motivo,string id_factura,string id_proveedor)
    {
        NotaCredito nota = new NotaCredito(idNota, num_nota, fecha, total_credito, motivo, id_factura, id_proveedor);
        return nota.Guardar();
    }
    [System.Web.Services.WebMethod()]
    public static string notasCredito()
    {
        // Pide los datos a la clase Factura y lo devuelve
        NotaCredito  notaC = new NotaCredito();
        return notaC.getNotaCredito();
    }
    [System.Web.Services.WebMethod()]
    public static string borrar(string idNota, string num_nota, string fecha, string total_credito, string motivo, string id_factura, string id_proveedor)
    {  
        // Pide los datos a la clase Factura y lo devuelve
        NotaCredito notaCr = new NotaCredito(idNota, num_nota, fecha, total_credito, motivo, id_factura,id_proveedor);
        return notaCr.Borrar();
    }
}
