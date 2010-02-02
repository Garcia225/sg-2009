using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for PagosCuotas
/// </summary>
public class PagosCuotas
{
	public PagosCuotas( string _idPagoCuota,
                        string _idCuota,
                        string _importe,
                        string _fecha,
                        string _idMovCtaCtePro)
	{
        idPagoCuota = Convert.ToInt32(_idPagoCuota);
        idCuota = Convert.ToInt32(_idCuota);
        importe = float.Parse(_importe);
        fecha = Convert.ToDateTime(_fecha);
        idMovCtaCtePro = Convert.ToInt32(_idMovCtaCtePro);

	}

    #region propiedades pago de cuotas
    /// <summary>
    /// ID del pago de la cuota
    /// </summary>
    private int idPagoCuota;
    public int IdPagoCuota
    {
        get { return idPagoCuota; }
    }

    /// <summary>
    /// ID de la cuota
    /// </summary>
    private int idCuota;
    public int IdCuota
    {
        set { idCuota = value; }
        get { return idCuota; }
    }

    /// <summary>
    /// Importe de la cuota
    /// </summary>
    private float importe;
    public float Importe
    {
        set { importe = value; }
        get { return importe; }
    }

    /// <summary>
    /// Fecha de la cuota
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
    private int idMovCtaCtePro;
    public int IdMovCtaCtePro
    {
        set { idMovCtaCtePro = value; }
        get { return idMovCtaCtePro; }
    }
    #endregion

    /// <summary>
    /// Registra un nuevo pago de una determinada cuota
    /// </summary>
    /// <returns></returns>
    public string Guardar()
    {
        //Creo la conexion
        Conexion _conexion = new Conexion();
        int opcion = 1;
        //string success;
        try
        {
            _conexion.OpenConnection();
            SqlCommand procPagoCuotas = new SqlCommand("sp_a_pago_cuotas", _conexion.getSqlConnection());
            procPagoCuotas.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procPagoCuotas.Parameters.Add(new SqlParameter("@opcion", opcion));//1
            procPagoCuotas.Parameters.Add(new SqlParameter("@id_pago_cuota", IdPagoCuota));//0
            procPagoCuotas.Parameters.Add(new SqlParameter("@id_cuota", IdCuota));//2
            procPagoCuotas.Parameters.Add(new SqlParameter("@importe", Importe));//90.0
            procPagoCuotas.Parameters.Add(new SqlParameter("@fecha", Fecha));//08/12/2009
            procPagoCuotas.Parameters.Add(new SqlParameter("@id_mov_cta_cte_pro", IdMovCtaCtePro));//1
            //Ejecuto la consulta
            procPagoCuotas.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return exc.ToString(); //"ERROR";
        }
    }
}
