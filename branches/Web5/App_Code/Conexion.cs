using System;
using System.Data;
using System.Data.Sql;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Clase conexion, que abre o cierra una conexion
/// </summary>
public class Conexion
{
    //Variables globales
    SqlConnection _conexion = new SqlConnection();
    string _conectionString = "";

	public Conexion()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// metodo que realiza la conexion
    /// utiliza el parametro insertado en el constructor
    /// </summary>
    public void OpenConnection()
    {
        // Crea un objeto conexion
        _conexion = new SqlConnection();

        // Creat una cadenaconexion
        _conectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\cta_cte_compras.mdf;Integrated Security=True;User Instance=True";
        _conexion.ConnectionString = _conectionString;
        // Abre la conexion
        try
        {
            //se habre la conexion
            _conexion.Open();

        }
        //capturamos los errores
        catch (Exception ex)
        {
            throw (ex);
        }
    }

    /// <summary>
    /// Cierra la conexion
    /// </summary>
    public void CloseConnection()
    {
        if (_conexion.State.Equals("Open"))
        {
            _conexion.Close();
        }
    }

    /// <summary>
    ///  Retorna el valor del SqlConection
    /// </summary>
    /// <returns></returns>
    public SqlConnection getSqlConnection()
    {
        //retorna la conexion
        return _conexion;
    }

    /// <summary>
    /// retorna el cadena conexion
    /// </summary>
    /// <returns>string</returns>
    public string getConectionString()
    {
        //devuelve el conexion string
        return _conectionString;
    }
}
