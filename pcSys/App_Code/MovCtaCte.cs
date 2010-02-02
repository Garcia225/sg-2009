using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for MovCtaCte
/// </summary>
public class MovCtaCte
{
	public MovCtaCte(   string id_mov_cta_cte_pro,
                        string num_cta_cte_pro,
                        string id_nota_credito,
                        string suma_resta,
                        string _fecha,
                        string id_factura,
                        string cant_cuotas)
	{
        idMovCtaCtePro = Convert.ToInt32(id_mov_cta_cte_pro);
        numCtaCtePro = Convert.ToInt32(num_cta_cte_pro);
        idNotaCredito = Convert.ToInt32(id_nota_credito);
        sumaResta = char.Parse(suma_resta);
        fecha = Convert.ToDateTime(_fecha);
        idFactura = Convert.ToInt32(id_factura);
        cantCuotas = Convert.ToInt32(cant_cuotas);
	}

    public MovCtaCte()
    {
    }
    //Propiedades de movimiento
    #region propiedades Mov Cta Cte
    /// <summary>
    /// ID del mov cta cte
    /// </summary>
    private int idMovCtaCtePro;
    public int IdMovCtaCtePro
    {
        get { return idMovCtaCtePro; }
    }

    /// <summary>
    /// ID de cta cte del proveedor
    /// </summary>
    private int numCtaCtePro;
    public int NumCtaCtePro
    {
        set { numCtaCtePro = value; }
        get { return numCtaCtePro; }
    }

    /// <summary>
    /// ID noto de Credito
    /// </summary>
    private int idNotaCredito;
    public int IdNotaCredito
    {
        set { idNotaCredito = value; }
        get { return idNotaCredito; }
    }

    /// <summary>
    /// Suma resta
    /// </summary>
    private char sumaResta;
    public char SumaResta
    {
        set { sumaResta = value; }
        get { return sumaResta; }
    }

    /// <summary>
    /// Fecha del movimiento
    /// </summary>
    private DateTime fecha;
    public DateTime Fecha
    {
        set { fecha = value; }
        get { return fecha; }
    }

    /// <summary>
    /// ID de la fatura del proveedor
    /// </summary>
    private int idFactura;
    public int IdFatura
    {
        set { idFactura = value; }
        get { return idFactura; }
    }

    /// <summary>
    /// Cantidad de cuotas
    /// </summary>
    private int cantCuotas;
    public int CantCuotas
    {
        set { cantCuotas = value; }
        get { return cantCuotas; }
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
            SqlCommand procMovCtaCte = new SqlCommand("sp_a_mov_cta_cte", _conexion.getSqlConnection());
            procMovCtaCte.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procMovCtaCte.Parameters.Add(new SqlParameter("@opcion", opcion));
            procMovCtaCte.Parameters.Add(new SqlParameter("@id_mov_cta_cte_pro", IdMovCtaCtePro));
            procMovCtaCte.Parameters.Add(new SqlParameter("@num_cta_cte_pro", NumCtaCtePro));
            procMovCtaCte.Parameters.Add(new SqlParameter("@id_nota_credito", IdNotaCredito));
            procMovCtaCte.Parameters.Add(new SqlParameter("@suma_resta", SumaResta));
            procMovCtaCte.Parameters.Add(new SqlParameter("@fecha", Fecha));
            procMovCtaCte.Parameters.Add(new SqlParameter("@id_factura", IdFatura));
            procMovCtaCte.Parameters.Add(new SqlParameter("@cant_cuotas", CantCuotas));
            //Ejecuto la consulta
            procMovCtaCte.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return "ERROR";
        }
    }




    /// <summary>
    /// Trae los datos de la cta cte de un proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string ObtenerCtaCte(int idProveedor)
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

            sentencia.Append(" SELECT debe, haber, saldo ");
            sentencia.Append(" FROM PCCC_CTA_CTE_PROVEEDOR where id_proveedor = @id_proveedor ");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@id_proveedor", idProveedor + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_CTA_CTE_PROVEEDOR");

            // Obtener el registro
            object[] datos = dataSet.Tables["PCCC_CTA_CTE_PROVEEDOR"].Rows[0].ItemArray;

            // Crea el diccionario de datos y agrega todas las entradas
            Dictionary<string, string> _values = new Dictionary<string, string>();

            _values.Add("debe", "" + datos[0]);
            _values.Add("haber", "" + datos[1]);
            _values.Add("saldo", "" + datos[2]);

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
    public string GetMovCtaCte(string idFactura)
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

            sentencia.Append(" SELECT id_mov_cta_cte_pro ");
            sentencia.Append(" FROM PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES where id_factura = " + Convert.ToInt32(idFactura));
            //10
            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES"]));
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
