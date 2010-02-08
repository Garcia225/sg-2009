using System;
using System.Data;
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
/// Summary description for NotaCredito
/// </summary>
public class NotaCredito
{
	public NotaCredito
	(               string id_notacredito,
                    string num_notacredito,
                    string _fecha,
                    string total_credito,
                    string _motivo,
                    string id_factura)
	{
        idNotaCredito= Convert.ToInt32(id_notacredito);
        numNotaCredito = Convert.ToInt32(num_notacredito);
        fecha = Convert.ToDateTime(_fecha);
        totalCredito = float.Parse(total_credito);
        motivo = _motivo;
        idFactura = Convert.ToInt32(id_factura);
	}
     public NotaCredito() { }
    #region propiedades nota de credito
    /// <summary>
    /// ID del la nota  de credito
    /// </summary>
    /// 
     private int idNotaCredito;
     public int IdNotaCredito
     {
         get { return idNotaCredito; }
     }
   

    /// <summary>
    /// Numero de  la nota de credito
    /// </summary>
    /// 

     private int numNotaCredito;
     public int NumNotaCredito
     {
         set { numNotaCredito = value; }
         get { return numNotaCredito; }
     }
   

  

    /// <summary>
    /// Fecha de emision de la factura
    /// </summary>
    private DateTime fecha;
    public DateTime Fecha
    {
        set { fecha = value; }
        get { return fecha; }
    }

    /// <summary>
    /// Total de la nota credito
    /// </summary>
    private float totalCredito;
    public float TotalCredito
    {
        set { totalCredito = value; }
        get { return totalCredito; }
    }

    /// <summary>
    /// Motivo de la nota credito
    /// </summary>
    private string motivo;
    public string Motivo
    {
        set { motivo = value; }
        get { return motivo; }
    }

    /// <summary>
    /// Id de la empleado
    /// </summary>
    private int idFactura;
    public int IdFactura
    {
        set { idFactura = value; }
        get { return idFactura; }
    }

    #endregion



    /// <summary>
    /// Guarda una NOTA de Credito
    /// </summary>
    /// <returns></returns>
    public string Guardar()
    {
        Conexion _conexion = null;
        int cont = 0;
        try
        {

            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();

            // Opcion a realizarce dentro del procedimiento almacenado
            int opcion = 1;

            //procedimiento cabecera
            SqlCommand proc = new SqlCommand("sp_abm_nota_credito", _conexion.getSqlConnection());
            proc.CommandType = CommandType.StoredProcedure;
            

            //cabecera   
            //pasar los parametros al procedimiento almacenado
            proc.Parameters.Add(new SqlParameter("@opcion", opcion));//1
            proc.Parameters.Add(new SqlParameter("@id_nota_credito", "" + IdNotaCredito));//1
            proc.Parameters.Add(new SqlParameter("@num_nota_credito", "" + NumNotaCredito));//1
            proc.Parameters.Add(new SqlParameter("@fecha", Fecha));//24/11/2009 0:00:00
            proc.Parameters.Add(new SqlParameter("@total", TotalCredito));//120000.0
            proc.Parameters.Add(new SqlParameter("@motivo", Motivo));//1
            proc.Parameters.Add(new SqlParameter("@id_factura", IdFactura));//1
            // No se encontró el procedimiento almacenado 'sp_abm_nota_credito'
            proc.ExecuteNonQuery();
            return "OK";

        }
        catch (Exception s) {
 
         return "ERROR";

        }
        return "ERROR";
    }

    /// <summary>
    /// Obtengo los datos para la tabla dinamica
    /// </summary>
    /// <returns>Retorna el dataset</returns>
    public string getNotaCredito()
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
           /* SELECT     PCCC_NOTA_CREDITO.num_nota_credito, PCCC_NOTA_CREDITO.fecha, PCCC_NOTA_CREDITO.total, PCCC_NOTA_CREDITO.motivo, 
                      PCCC_FACTURA.num_factura, PCCC_NOTA_CREDITO.id_nota_credito
FROM         PCCC_NOTA_CREDITO INNER JOIN
                      PCCC_FACTURA ON PCCC_NOTA_CREDITO.id_factura = PCCC_FACTURA.id_factura
WHERE     (PCCC_NOTA_CREDITO.borrado = 'N')*/

            //char n = 'N';

            sentencia.Append(" SELECT PCCC_NOTA_CREDITO.num_nota_credito, ");
            sentencia.Append(" PCCC_NOTA_CREDITO.fecha, ");
            sentencia.Append(" PCCC_NOTA_CREDITO.total, ");
            sentencia.Append(" PCCC_NOTA_CREDITO.motivo, ");
            sentencia.Append(" PCCC_FACTURA.num_factura, ");
            sentencia.Append(" PCCC_NOTA_CREDITO.id_nota_credito ");
            sentencia.Append(" FROM PCCC_NOTA_CREDITO, PCCC_FACTURA where PCCC_NOTA_CREDITO.borrado = 'N'");
            //sentencia.Append(" FROM PCCC_NOTA_CREDITO, PCCC_FACTURA where PCCC_NOTA_CREDITO.borrado = "+n);
            sentencia.Append(" AND PCCC_NOTA_CREDITO.id_factura = PCCC_FACTURA.id_factura");

            // Invalid column name 'borrado'
            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_NOTA_CREDITO");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_NOTA_CREDITO"]));
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
    /// Borra una nota de credito
    /// 
    /// </summary>
    /// <returns></returns>

    public string Borrar()
    {
        Conexion _conexion = null;
        int cont = 0;
        try
        {

            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();

            // Opcion a realizarce dentro del procedimiento almacenado
            int opcion = 3;

            //procedimiento cabecera
            SqlCommand proc = new SqlCommand("sp_abm_nota_credito", _conexion.getSqlConnection());
            proc.CommandType = CommandType.StoredProcedure;


            //cabecera   
            //pasar los parametros al procedimiento almacenado
            proc.Parameters.Add(new SqlParameter("@opcion", opcion));//1
            proc.Parameters.Add(new SqlParameter("@id_nota_credito", "" + IdNotaCredito));//1
            proc.Parameters.Add(new SqlParameter("@num_nota_credito", "" + NumNotaCredito));//1
            proc.Parameters.Add(new SqlParameter("@fecha", Fecha));//24/11/2009 0:00:00
            proc.Parameters.Add(new SqlParameter("@total", TotalCredito));//120000.0
            proc.Parameters.Add(new SqlParameter("@motivo", "jj"));//1
            proc.Parameters.Add(new SqlParameter("@id_factura", IdFactura));//1

            proc.ExecuteNonQuery();
            return "OK";

        }
        catch (Exception s)
        {

            return "ERROR";

        }
        return "ERROR";
    }


}

