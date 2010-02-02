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
using AjaxPro;

public partial class CtaCteCompras_Pagos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Devuelve un arreglo con las cuotas vencidas de un determinado Proveedor
    /// </summary>
    /// <param name="fecha"></param>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string facturasVencidadas(string fecha, int idProveedor)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura();
        return factura.obtenerVencimiento(Convert.ToDateTime(fecha), idProveedor);
        //return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// Devuelve un arreglo con todas las cuotas de ese proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string facturasPendientes(int idProveedor)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura();
        return factura.getFacturas(idProveedor);
        //return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// Obtiene las cuotas de una factura
    /// </summary>
    /// <param name="idFactura"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string obtenerCuotas(int idFactura)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura();
        return factura.getCuotas(idFactura);
        //return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// Registra el pago parcial o total de una cuota
    /// </summary>
    /// <param name="idPagoCuota"></param>
    /// <param name="idCuota"></param>
    /// <param name="importe"></param>
    /// <param name="idMovCtaCtePro"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string pagosCuotas(string idPagoCuota, string idCuota, string importe, string fecha, string idMovCtaCtePro)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        PagosCuotas pago = new PagosCuotas(idPagoCuota, idCuota, importe, fecha, idMovCtaCtePro);
        return pago.Guardar();
        //return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// Obtiene el id del mov de la cta cte de una factura
    /// </summary>
    /// <param name="idFactura"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getMov(string idFactura)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        //Cuotas cuota = new Cuotas();
        Factura fatura = new Factura();
        return fatura.ObtenerMovCtaCte(Convert.ToInt32(idFactura)); //cuota.ObtenerMovCtaCte(Convert.ToInt32(idCuota));
        //return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// Datos de la cta cte de un proveedor
    /// </summary>
    /// <param name="idFactura"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getCtaCtePro(string idProveedor)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        //Cuotas cuota = new Cuotas();
        MovCtaCte mov = new MovCtaCte();
        return mov.ObtenerCtaCte(Convert.ToInt32(idProveedor)); //cuota.ObtenerMovCtaCte(Convert.ToInt32(idCuota));
        //return Proveedores.Obtener(idProveedor);
    }
}
