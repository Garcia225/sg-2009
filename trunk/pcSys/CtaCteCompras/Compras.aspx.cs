using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using AjaxPro;
using log4net;


public partial class Compras : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.RegisterTypeForAjax(typeof(Compras));
            //Utility.RegisterTypeForAjax(typeof(Factura));
        }
    }


    /// <summary>
    /// Trae una lista de todos los datos de una persona
    /// </summary>
    /// <param name="idCliente"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetNombreProveedor()
    {
        // Pide los datos a la clase Cliente y lo devuelve
        
        Serializador serial = new Serializador();
        StringBuilder sb = new StringBuilder();
        Conexion conexion = null;

        try
        {

            conexion = new Conexion();

            //adaptador de datos
            SqlDataAdapter comando = new SqlDataAdapter();

            //crea el data set
            DataSet dataSet = new DataSet();

            //abre la conexion
            conexion.OpenConnection();

            conexion.getSqlConnection().BeginTransaction();

            //crear sentencia sql
            StringBuilder sentencia = new StringBuilder();

            sentencia.Append(" SELECT id_proveedor, ");
            sentencia.Append(" nombre, apellido, razon_social ");
            sentencia.Append(" FROM PCCC_PROVEEDORES where borrado = 'N'");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_PROVEEDORES");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_PROVEEDORES"]));
        }
        catch (Exception error)
        {
            sb.Append(error);
        }
        finally
        {
            conexion.CloseConnection();
        }


        return sb.ToString();

    //}
        //return Proveedores.getNombreProveedor();
    }

    /// <summary>
    /// Obtengo las targetas que posee la empresa
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetTargetas()
    {
        // Pide los datos a la clase Cliente y lo devuelve

        Serializador serial = new Serializador();
        StringBuilder sb = new StringBuilder();
        Conexion conexion = null;

        try
        {

            conexion = new Conexion();

            //adaptador de datos
            SqlDataAdapter comando = new SqlDataAdapter();

            //crea el data set
            DataSet dataSet = new DataSet();

            //abre la conexion
            conexion.OpenConnection();

            conexion.getSqlConnection().BeginTransaction();

            //crear sentencia sql
            StringBuilder sentencia = new StringBuilder();

            sentencia.Append(" SELECT num_tarjeta ");
            sentencia.Append(" FROM PCCC_CUPON_DE_CREDITO");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_CUPON_DE_CREDITO");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_CUPON_DE_CREDITO"]));
        }
        catch (Exception error)
        {
            sb.Append(error);
        }
        finally
        {
            conexion.CloseConnection();
        }


        return sb.ToString();

        //}
        //return Proveedores.getNombreProveedor();
    }

    /// <summary>
    /// Obtengo los cheques que posee la empresa
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetBancos()
    {
        // Pide los datos a la clase Cliente y lo devuelve

        Serializador serial = new Serializador();
        StringBuilder sb = new StringBuilder();
        Conexion conexion = null;

        try
        {

            conexion = new Conexion();

            //adaptador de datos
            SqlDataAdapter comando = new SqlDataAdapter();

            //crea el data set
            DataSet dataSet = new DataSet();

            //abre la conexion
            conexion.OpenConnection();

            conexion.getSqlConnection().BeginTransaction();

            //crear sentencia sql
            StringBuilder sentencia = new StringBuilder();

            sentencia.Append(" SELECT id_banco, banco ");
            sentencia.Append(" FROM PCCC_BANCO");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_BANCO");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_BANCO"]));
        }
        catch (Exception error)
        {
            sb.Append(error);
        }
        finally
        {
            conexion.CloseConnection();
        }


        return sb.ToString();

        //}
        //return Proveedores.getNombreProveedor();
    }

    /// <summary>
    /// Obtiene el id de la cuenta corriente de un proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetCtaCteProveedor(int idProveedor)
    {
        // Pide los datos a la clase Cliente y lo devuelve

        Serializador serial = new Serializador();
        StringBuilder sb = new StringBuilder();
        Conexion conexion = null;

        try
        {

            conexion = new Conexion();

            /////////////////////////////////////////////////////////////////////////////////////////

            string consultaCtaCte = "Select num_cta_cte_pro from CCP_CTA_CTE_PROVEEDOR where id_proveedor = "+ idProveedor;

            SqlCommand consulta = new SqlCommand(consultaCtaCte, conexion.getSqlConnection());
            SqlDataReader reader = consulta.ExecuteReader();
            string numero = "";
            int getIdCtaCte = 0;
            while (reader.Read())
            {
                numero = reader[0].ToString();
            }
            reader.Close();
            getIdCtaCte = Convert.ToInt32(numero);

            /////////////////////////////////////////////////////////////////////////////////////////
            //adaptador de datos
            SqlDataAdapter comando = new SqlDataAdapter();

            //crea el data set
            DataSet dataSet = new DataSet();

            //abre la conexion
            conexion.OpenConnection();

            conexion.getSqlConnection().BeginTransaction();

            //crear sentencia sql
            StringBuilder sentencia = new StringBuilder();

            sentencia.Append(" SELECT debe, ");
            sentencia.Append(" haber, saldo ");
            sentencia.Append(" FROM ViewCtaCte where num_cta_cte_pro = @num_cta_cte_pro");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            //comando.SelectCommand.Parameters.Add("@id_materia_prima", idComponente + "");

            //llena el dataset
            comando.Fill(dataSet, "ViewCtaCte");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["ViewCtaCte"]));
        }
        catch (Exception error)
        {
            sb.Append(error);
        }
        finally
        {
            conexion.CloseConnection();
        }


        return sb.ToString();

        //}
        //return Proveedores.getNombreProveedor();
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
    /// Devuelve el costo del componente requerido
    /// </summary>
    /// <param name="idComponente"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string getCostoComponente(int idComponente)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura();
        return factura.obtenerCostoComponente(idComponente);
    }

    /// <summary>
    /// Guarda una nueva factura proveedor
    /// </summary>
    /// <param name="id_factura"></param>
    /// <param name="proveedor"></param>
    /// <param name="fecha"></param>
    /// <param name="factura"></param>
    /// <param name="condicion_pago"></param>
    /// <param name="empleado"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GuardarFactura(string id_factura, string proveedor, string num_factura, string fecha, 
        string total_factura, string condicion_pago, string empleado, string detalle_factura, string num_cheque,
        string id_banco, string opcion,string numCuota,string cantCuotas,string importe,string saldo,
        string fechaVencimiento,string idFormaPago,string sumaResta,string idMovCtaCtePro)
    {
        
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura(id_factura, proveedor, num_factura, fecha, total_factura, condicion_pago, empleado, detalle_factura, cantCuotas, sumaResta);
        factura.Guardar();
        //aqui se debe guardar el mov
        if (opcion == "Credito")
        {
            Cuotas cuota = new Cuotas("1", numCuota, cantCuotas, importe, saldo, fechaVencimiento, idFormaPago, idMovCtaCtePro);
            cuota.Guardar();
            return factura.Guardar();
        }
        else
        {

            Cheques cheque = new Cheques(total_factura, num_cheque, fecha, id_banco);
            factura.Guardar();
            return cheque.Guardar();
        }
        return factura.Guardar();
    }

    /// <summary>
    /// Devuelve el nombre de un producto deterninado
    /// </summary>
    /// <param name="idProducto"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string nombreProducto(int idProducto)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        Factura factura = new Factura();
        return factura.obtenerNombreProducto(idProducto);
    }

    /// <summary>
    /// Obtiene el id del movimiento
    /// </summary>
    /// <param name="idFactura"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string movCtaCte(string idFactura)
    {
        // Pide los datos a la clase Cliente y lo devuelve
        MovCtaCte mov = new MovCtaCte();
        return mov.GetMovCtaCte(idFactura);
    }

    //GetMovCtaCte(string idFactura)
}

