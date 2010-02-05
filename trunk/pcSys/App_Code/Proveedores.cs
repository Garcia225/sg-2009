using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for Proveedores
/// </summary>
public class Proveedores
{
    public Proveedores( string id_proveedor,
                        string razon_social,
                        string _nombre,
                        string _apellido,
                        string num_doc,
                        string _direccion,
                        string _telefono,
                        string _id_tipo_doc)
	{
        //select id_proveedor from viewFatura where contado = 'N' and id_proveedor = 17
        idProveedor = Convert.ToInt32(id_proveedor);
        razonSocial = razon_social;
        nombre = _nombre;
        apellido = _apellido;
        numDoc = Convert.ToInt32(num_doc);
        direccion = _direccion;
        telefono = _telefono;
        idTipoDoc = _id_tipo_doc;
	}
    public Proveedores()
    {
    }

    #region propiedades personas
    /// <summary>
    /// ID del proveedor
    /// </summary>
    private int idProveedor;
    public int IdProvedor
    {
        get { return idProveedor; }
    }

    /// <summary>
    /// Numero del documento de la persona
    /// </summary>
    private int numDoc;
    public int NumDoc
    {
        get { return numDoc; }
    }

    /// <summary>
    /// Razon social
    /// </summary>
    private string razonSocial;
    public string RazonSocial
    {
        set { razonSocial = value; }
        get { return razonSocial; }
    }

    /// <summary>
    /// Tipo de documento
    /// </summary>
    private string tipoDoc;
    public string TipoDoc
    {
        set { tipoDoc = value; }
        get { return tipoDoc; }
    }

    /// <summary>
    /// Nombre de la persona
    /// </summary>
    private string nombre;
    public string Nombre
    {
        set { nombre = value; }
        get { return nombre; }
    }

    /// <summary>
    /// Apellido del cliente
    /// </summary>
    private string apellido;
    public string Apellido
    {
        set { apellido = value; }
        get { return apellido; }
    }

    /// <summary>
    /// Direccion del cliente
    /// </summary>
    private string direccion;
    public string Direccion
    {
        set { direccion = value; }
        get { return direccion; }
    }

    /// <summary>
    /// Telefono de cliente
    /// </summary>
    private string telefono;
    public string Telefono
    {
        set { telefono = value; }
        get { return telefono; }
    }

    /// <summary>
    /// Nacionalidad de la persona
    /// </summary>
    private string idTipoDoc;
    public string IdTipoDoc
    {
        set { idTipoDoc = value; }
        get { return idTipoDoc; }
    }

    #endregion



    /// <summary>
    /// Guarda un registro
    /// </summary>
    public string Guardar()
    {
        //Creo la conexion
        Conexion _conexion = new Conexion();
        int opcion = 1;
        //string success;
        try
        {
            _conexion.OpenConnection();
            SqlCommand procProveedor = new SqlCommand("sp_abm_proveedor", _conexion.getSqlConnection());
            procProveedor.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procProveedor.Parameters.Add(new SqlParameter("@opcion", opcion));
            procProveedor.Parameters.Add(new SqlParameter("@id_proveedor", IdProvedor));
            procProveedor.Parameters.Add(new SqlParameter("@razon_social", RazonSocial));
            procProveedor.Parameters.Add(new SqlParameter("@nombre", Nombre));
            procProveedor.Parameters.Add(new SqlParameter("@apellido", Apellido));
            procProveedor.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
            procProveedor.Parameters.Add(new SqlParameter("@direccion", Direccion));
            procProveedor.Parameters.Add(new SqlParameter("@telefono", Telefono));
            procProveedor.Parameters.Add(new SqlParameter("@id_tipo_documento", IdTipoDoc));
            //Ejecuto la consulta
            procProveedor.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return "ERROR";
        }
    }

    /// <summary>
    /// Modifica un registro
    /// </summary>
    public string Modificar()
    {
        //Creo la conexion
        Conexion _conexion = new Conexion();
        int opcion = 2;
        //string success;
        try
        {
            _conexion.OpenConnection();
            SqlCommand procProveedor = new SqlCommand("sp_abm_proveedor", _conexion.getSqlConnection());
            procProveedor.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procProveedor.Parameters.Add(new SqlParameter("@opcion", opcion));
            procProveedor.Parameters.Add(new SqlParameter("@id_proveedor", IdProvedor));
            procProveedor.Parameters.Add(new SqlParameter("@razon_social", RazonSocial));
            procProveedor.Parameters.Add(new SqlParameter("@nombre", Nombre));
            procProveedor.Parameters.Add(new SqlParameter("@apellido", Apellido));
            procProveedor.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
            procProveedor.Parameters.Add(new SqlParameter("@direccion", Direccion));
            procProveedor.Parameters.Add(new SqlParameter("@telefono", Telefono));
            procProveedor.Parameters.Add(new SqlParameter("@id_tipo_documento", IdTipoDoc));
            //Ejecuto la consulta
            procProveedor.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return "ERROR";
        }
    }

    /// <summary>
    /// Borra un registro
    /// </summary>
    public string Borrar()
    {
        //Creo la conexion
        Conexion _conexion = new Conexion();
        int opcion = 3;
        //string success;
        try
        {
            _conexion.OpenConnection();
            SqlCommand procProveedor = new SqlCommand("sp_abm_proveedor", _conexion.getSqlConnection());
            procProveedor.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procProveedor.Parameters.Add(new SqlParameter("@opcion", opcion));
            procProveedor.Parameters.Add(new SqlParameter("@id_proveedor", IdProvedor));
            procProveedor.Parameters.Add(new SqlParameter("@razon_social", RazonSocial));
            procProveedor.Parameters.Add(new SqlParameter("@nombre", Nombre));
            procProveedor.Parameters.Add(new SqlParameter("@apellido", Apellido));
            procProveedor.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
            procProveedor.Parameters.Add(new SqlParameter("@direccion", Direccion));
            procProveedor.Parameters.Add(new SqlParameter("@telefono", Telefono));
            procProveedor.Parameters.Add(new SqlParameter("@id_tipo_documento", IdTipoDoc));
            //Ejecuto la consulta
            procProveedor.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return "ERROR";
        }
    }

    /// <summary>
    /// Obtengo los datos para la tabla dinamica
    /// </summary>
    /// <returns>Retorna el dataset</returns>
    public static string DataSet()
    {

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

            sentencia.Append(" SELECT id_proveedor, razon_social, apellido,");
            sentencia.Append(" direccion, telefono, borrado ");
            sentencia.Append(" FROM ViewProveedores where borrado = 'N'");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "ViewProveedores");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["ViewProveedores"]));
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

    }



    /// <summary>
    /// Devuelve los datos de una persona
    /// </summary>
    /// <param name="numDoc"></param>
    /// <returns></returns>
    public static string Obtener(int idProveedor)
    {

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

            sentencia.Append(" SELECT id_proveedor, razon_social, nombre, apellido, num_doc ,");
            sentencia.Append(" direccion, telefono, id_tipo_doc");
            sentencia.Append(" FROM ViewProveedores where id_proveedor = @id_proveedor and borrado = 'N'");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@id_proveedor", idProveedor + "");

            //llena el dataset
            comando.Fill(dataSet, "ViewProveedores");




            // Obtener el registro
            object[] datos = dataSet.Tables["ViewProveedores"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            _values.Add("idProveedor", "" + datos[0]);
            _values.Add("razonSocial", "" + datos[1]);
            _values.Add("nombre", "" + datos[2]);
            _values.Add("apellido", "" + datos[3]);
            _values.Add("numDoc", "" + datos[4]);
            _values.Add("direccion", "" + datos[5]);
            _values.Add("telefono", "" + datos[6]);
            _values.Add("idTipoDocumento", "" + datos[7]);
            //_values.Add("nacionalidad", "" + datos[7]);

            // Serializador de JavaScript
            JavaScriptSerializer ser = new JavaScriptSerializer();

            // Serializo los datos al formato json
            sb.Append(ser.Serialize(_values));
            string j = sb.ToString();
            conexion.CloseConnection();
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
    }



    /// <summary>
    /// Obtengo los datos para la tabla dinamica
    /// </summary>
    /// <returns>Retorna el dataset</returns>
    public static string getNombreProveedor()
    {

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
            sentencia.Append(" nombre ");
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

    }

    /// <summary>
    /// Obtengo el id de la cta cte del proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string getIdCtaCte(int idProveedor)
    {

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

            sentencia.Append(" SELECT num_cta_cte_pro from PCCC_CTA_CTE_PROVEEDOR");
            sentencia.Append(" where id_proveedor = "+ Convert.ToInt32(idProveedor));

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_CTA_CTE_PROVEEDOR");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_CTA_CTE_PROVEEDOR"]));
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

    }

    /// <summary>
    /// Comprueba si el proveedor posee facturas pendientes de pago
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string comprobarDeudas(string idProveedor)
    {

        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();
            string consultaEstado = "select id_proveedor from viewFatura where contado = 'N' and id_proveedor = " + idProveedor ;
            // El nombre de columna 'PAGADO' no es válido
            SqlCommand consulta = new SqlCommand(consultaEstado, _conexion.getSqlConnection());
            SqlDataReader reader = consulta.ExecuteReader();
            string numero = "";
            while (reader.Read())
            {
                numero = reader[0].ToString();
            }
            reader.Close();
            return numero;
        }
        catch (Exception exc)
        {
            return "ERROR";
        }
    }
}
