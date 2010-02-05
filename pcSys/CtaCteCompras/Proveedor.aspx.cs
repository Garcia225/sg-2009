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

public partial class Personas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Utility.RegisterTypeForAjax(typeof(Personas));
            Utility.RegisterTypeForAjax(typeof(Proveedores));
         }
    }

    /// <summary>
    /// obtiene todos las personas registradas
    /// </summary>
    [System.Web.Services.WebMethod()]
    public static string getPersonas()
    {
        // Pide el dataset a la clase Proveedor y lo devuelve
        return Proveedores.DataSet();
    }

    /// <summary>
    /// Trae una lista de todos los datos de una persona
    /// </summary>
    /// <param name="idCliente"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string DatosProveedor(int idProveedor)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        return Proveedores.Obtener(idProveedor);
    }

    /// <summary>
    /// guarda un nuevo registro en la tabla personas
    /// </summary>
    /// <param name="num_doc"></param>
    /// <param name="tipo_doc"></param>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="dir"></param>
    /// <param name="email"></param>
    /// <param name="nacionalidad"></param>
    /// <param name="sexo"></param>
    /// <param name="fechaNac"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string guardar(string idProveedor, string razon_social, string nombre, string apellido, string num_doc, string dir, string telefono, string tipo_doc)
    {
        Proveedores proveedor = new Proveedores(idProveedor, razon_social, nombre, apellido, num_doc, dir, telefono, tipo_doc);
        //Persona persona = new Persona(num_doc, tipo_doc, nombre, apellido, dir, email, nacionalidad, sexo, fechaNac);
        return proveedor.Guardar();
    }


    /// <summary>
    /// Borra un registro de la tabla persona
    /// </summary>
    /// <param name="num_doc"></param>
    /// <param name="tipo_doc"></param>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="dir"></param>
    /// <param name="email"></param>
    /// <param name="nacionalidad"></param>
    /// <param name="sexo"></param>
    /// <param name="fechaNac"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string borrar(string idProveedor, string razon_social, string nombre, string apellido, string num_doc, string dir, string telefono, string tipo_doc)
    {
        Proveedores prov = new Proveedores();
        string deudas = prov.comprobarDeudas(idProveedor);
        if (deudas != "")
        {
            return "ERROR";
        }
        else {
            Proveedores proveedor = new Proveedores(idProveedor, razon_social, nombre, apellido, num_doc, dir, telefono, tipo_doc);

            //Persona persona = new Persona(num_doc, tipo_doc, nombre, apellido, dir, email, nacionalidad, sexo, fechaNac);
            proveedor.Borrar();
            return "EXITO";
            //return "SI";
        }
    }
   
    /// <summary>
    /// Modifica los datos de un registro
    /// </summary>
    /// <param name="num_doc"></param>
    /// <param name="tipo_doc"></param>
    /// <param name="nombre"></param>
    /// <param name="apellido"></param>
    /// <param name="dir"></param>
    /// <param name="email"></param>
    /// <param name="nacionalidad"></param>
    /// <param name="sexo"></param>
    /// <param name="fechaNac"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string modificar(string idProveedor, string razon_social, string nombre, string apellido, string num_doc, string  dir, string telefono, string tipo_doc)
    {
        Proveedores proveedor = new Proveedores(idProveedor, razon_social, nombre, apellido, num_doc,  dir, telefono,tipo_doc);
        return proveedor.Modificar();
    }
}
