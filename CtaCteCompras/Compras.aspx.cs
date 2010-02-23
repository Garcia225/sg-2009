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
            Utility.RegisterTypeForAjax(typeof(Factura));
        }
    }
    
    ///
    [System.Web.Services.WebMethod()]
    public static string getFacturas()
    {
        // Pide el dataset a la clase Proveedor y lo devuelve
        return Factura.DataSet();
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

            /*sentencia.Append(" SELECT id_proveedor, ");
            sentencia.Append(" nombre, apellido, razon_social ");
            sentencia.Append(" FROM PCCC_PROVEEDORES where borrado = 'N'");
            */
            sentencia.Append(" SELECT id_proveedor, ");
            sentencia.Append(" apellido, razon_social ");
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
    public static string GetTargetas(string idTipoTar)
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

            sentencia.Append(" SELECT tar.num_targeta, tar.id_banco, ban.banco ");
            sentencia.Append(" FROM   PCCC_TARGETA AS tar,PCCC_BANCO AS ban ");
            sentencia.Append(" WHERE  tar.id_banco = ban.id_banco ");
            sentencia.Append(" AND    TAR.id_tipo_targeta = " + idTipoTar);

            
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
        string fechaVencimiento,string idFormaPago,string sumaResta,string idMovCtaCtePro,string interes,
        string pagosValores)//"[[\"3\",4,\"120000\",\"360000\"]]"     "[[\"CHEQUE\",1,\"1334457786\",120000,\"\"]]"
    {
        Factura fac = new Factura();
       
        MovCtaCte mov = new MovCtaCte();
        int i = 0;
        if (opcion == "Credito")
        {
            string estado = fac.getEstado("PENDIENTE");
            string condicion = fac.getCondicion("N");
            // Pide los datos a la clase Cliente y lo devuelve
            Factura factura = new Factura(id_factura, proveedor, num_factura, fecha, total_factura, condicion, empleado, detalle_factura, cantCuotas, sumaResta, estado);
            factura.Guardar();
            //fac.getEstado(opcion.ToUpper());
            //factura.Guardar();
            string ultimo = factura.ultimoGuardado();//75
            string idMov = mov.GetIdMovCtaCte(ultimo);//11
            fac.updateHaber(total_factura,proveedor);
            //obtener de alguna forma el mov cta cte pro
            for (i = 0; i < (Convert.ToInt32(cantCuotas)); i++ ) {
                int importeCuota = ((Convert.ToInt32(importe)) / (Convert.ToInt32(cantCuotas)));
                Cuotas cuota = new Cuotas("1", (i + 1).ToString(), cantCuotas, importeCuota.ToString(), importeCuota.ToString(), fechaVencimiento, idFormaPago, estado, idMov, interes);
                cuota.Guardar();
            }
        return "EXITO";
             
        }
        else
        {
            //Obtiene el id del estado PENDIENTE
            string estado = fac.getEstado("PENDIENTE");
            // Pide los datos a la clase Cliente y lo devuelve
            Factura factura = new Factura(id_factura, proveedor, num_factura, fecha, total_factura, condicion_pago, empleado, detalle_factura, cantCuotas, sumaResta, estado);
            factura.Guardar();
            string ultimo = factura.ultimoGuardado();
            factura.guardarValores(pagosValores, ultimo);
            return "EXITO";
        }
        return "ERROR";
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

    [System.Web.Services.WebMethod()]
    public static string AnularFactura(string id_factura)
    {//11
        Factura fac = new Factura();
        string estado = fac.getEstado("ANULADO");
        Factura factura = new Factura(id_factura, "0", "0", "01/01/2009", "0", "0", "0", "0", "0", "0", estado);
        factura.Anular();
        return "EXITO";
    }

    /// <summary>
    /// Obtengo los tipos de targeta
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod()]
    public static string GetTipoTargetas()
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

            sentencia.Append(" SELECT    id_tipo_targeta, descripcion ");
            sentencia.Append(" FROM  PCCC_TIPO_TARGETA");
            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_TIPO_TARGETA");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_TIPO_TARGETA"]));
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
}

