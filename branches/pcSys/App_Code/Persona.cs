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
/// Summary description for Personas
/// </summary>
public class Persona
{
	public Persona( string num_doc,
                    string tipo_doc,
                    string _nombre,
                    string _apellido,
                    string _direccion,
                    string _email,
                    string _nacionalidad,
                    string _sexo,
                    string _fecha_nacimiento)
	{
        int _numDoc = Convert.ToInt32(num_doc);
        DateTime _fechaNac = Convert.ToDateTime(_fecha_nacimiento);

        numDoc = _numDoc;
        tipoDoc = tipo_doc;
        nombre = _nombre;
        apellido = _apellido;
        direccion = _direccion;
        email = _email;
        nacionalidad = _nacionalidad;
        sexo = _sexo;
        fecha_nacimiento = _fechaNac;
	}

    #region propiedades personas

    /// <summary>
    /// Numero del documento de la persona
    /// </summary>
    private int numDoc;
    public int NumDoc
    {
        get { return numDoc; }
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
    /// Email de cliente
    /// </summary>
    private string email;
    public string Email
    {
        set { email = value; }
        get { return email; }
    }

    /// <summary>
    /// Nacionalidad de la persona
    /// </summary>
    private string nacionalidad;
    public string Nacionalidad
    {
        set { nacionalidad = value; }
        get { return nacionalidad; }
    }

    /// <summary>
    /// Sexo de la persona
    /// </summary>
    private string sexo;
    public string Sexo
    {
        set { sexo = value; }
        get { return sexo; }
    }

    /// <summary>
    /// Fecha de Nacimento del cliente
    /// </summary>
    private DateTime fecha_nacimiento;
    public DateTime FechaNacimiento
    {
        set { fecha_nacimiento = value; }
        get { return fecha_nacimiento; }
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
                SqlCommand procPersona = new SqlCommand("sp_abm_persona", _conexion.getSqlConnection());
                procPersona.CommandType = CommandType.StoredProcedure;

                //pasa los parametros al procedimiento almacenado
                //La opcion 1 es para guardar nuevo cliente
                procPersona.Parameters.Add(new SqlParameter("@opcion", opcion));
                procPersona.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
                procPersona.Parameters.Add(new SqlParameter("@tipo_doc", TipoDoc));
                procPersona.Parameters.Add(new SqlParameter("@nombre", Nombre));
                procPersona.Parameters.Add(new SqlParameter("@apellido", Apellido));
                procPersona.Parameters.Add(new SqlParameter("@direccion", Direccion));
                procPersona.Parameters.Add(new SqlParameter("@email", Email));
                procPersona.Parameters.Add(new SqlParameter("@nacionalidad", Nacionalidad));
                procPersona.Parameters.Add(new SqlParameter("@sexo", Sexo));
                procPersona.Parameters.Add(new SqlParameter("@fecha_nacimiento", FechaNacimiento));
                //Ejecuto la consulta
                procPersona.ExecuteNonQuery();

                
                return "OK";
            }
            catch(Exception exc)
            {
                return "ERROR";
            }
        }

    /// <summary>
    /// Modifica un registro
    /// </summary>
    /// <returns></returns>
    public string Modificar()
    {
        //Creo la conexion
        Conexion _conexion = new Conexion();
        int opcion = 2;
        //string success;
        try
        {
            _conexion.OpenConnection();
            SqlCommand procPersona = new SqlCommand("sp_abm_persona", _conexion.getSqlConnection());
            procPersona.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procPersona.Parameters.Add(new SqlParameter("@opcion", opcion));
            procPersona.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
            procPersona.Parameters.Add(new SqlParameter("@tipo_doc", TipoDoc));
            procPersona.Parameters.Add(new SqlParameter("@nombre", Nombre));
            procPersona.Parameters.Add(new SqlParameter("@apellido", Apellido));
            procPersona.Parameters.Add(new SqlParameter("@direccion", Direccion));
            procPersona.Parameters.Add(new SqlParameter("@email", Email));
            procPersona.Parameters.Add(new SqlParameter("@nacionalidad", Nacionalidad));
            procPersona.Parameters.Add(new SqlParameter("@sexo", Sexo));
            procPersona.Parameters.Add(new SqlParameter("@fecha_nacimiento", FechaNacimiento));
            //Ejecuto la consulta
            procPersona.ExecuteNonQuery();


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
                SqlCommand procPersona = new SqlCommand("sp_abm_persona", _conexion.getSqlConnection());
                procPersona.CommandType = CommandType.StoredProcedure;

                //pasa los parametros al procedimiento almacenado
                //La opcion 1 es para guardar nuevo cliente
                procPersona.Parameters.Add(new SqlParameter("@opcion", opcion));
                procPersona.Parameters.Add(new SqlParameter("@num_doc", NumDoc));
                procPersona.Parameters.Add(new SqlParameter("@tipo_doc", TipoDoc));
                procPersona.Parameters.Add(new SqlParameter("@nombre", Nombre));
                procPersona.Parameters.Add(new SqlParameter("@apellido", Apellido));
                procPersona.Parameters.Add(new SqlParameter("@direccion", Direccion));
                procPersona.Parameters.Add(new SqlParameter("@email", Email));
                procPersona.Parameters.Add(new SqlParameter("@nacionalidad", Nacionalidad));
                procPersona.Parameters.Add(new SqlParameter("@sexo", Sexo));
                procPersona.Parameters.Add(new SqlParameter("@fecha_nacimiento", FechaNacimiento));
                //Ejecuto la consulta
                procPersona.ExecuteNonQuery();


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

                sentencia.Append(" SELECT num_doc, nombre, apellido, ");
                sentencia.Append(" direccion ");
                sentencia.Append(" FROM ViewPersonas where estado = 'ACTIVO'");

                //carga el data set

                comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
                //llena el dataset
                comando.Fill(dataSet, "ViewPersonas");


                //serializar dataset
                sb.Append(serial.JSON(dataSet.Tables["ViewPersonas"]));
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
    public static string Obtener(int numDoc)
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

                sentencia.Append(" SELECT num_doc, tipo_doc, nombre, apellido, direccion ,");
                sentencia.Append(" email, nacionalidad, sexo , fecha_nacimiento ");
                sentencia.Append(" FROM viewPersonas where num_doc = @num_doc and estado = 'ACTIVO'");

                //carga el data set

                comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

                comando.SelectCommand.Parameters.Add("@num_doc", numDoc + "");

                //llena el dataset
                comando.Fill(dataSet, "viewPersonas");




                // Obtener el registro
                object[] datos = dataSet.Tables["viewPersonas"].Rows[0].ItemArray;

                // Crea el diccionario de datos y agrega todas las entradas
                Dictionary<string, string> _values = new Dictionary<string, string>();

                _values.Add("numDoc", "" + datos[0]);
                _values.Add("tipoDoc", "" + datos[1]);
                _values.Add("nombre", "" + datos[2]);
                _values.Add("apellido", "" + datos[3]);
                _values.Add("direccion", "" + datos[4]);
                _values.Add("email", "" + datos[5]);
                _values.Add("nacionalidad", "" + datos[6]);
                _values.Add("sexo", "" + datos[7]);
                _values.Add("fechaNacimiento", "" + datos[8]);

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
}
