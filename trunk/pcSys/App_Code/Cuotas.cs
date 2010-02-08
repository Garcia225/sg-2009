using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Cuotas
/// </summary>
public class Cuotas
{
	public Cuotas(  string _id_cuotas,
                    string _num_cuotas,
                    string _cant_cuotas,
                    string _importe,
                    string _saldo,
                    string _fecha_vecimiento,
                    string _id_forma_pago,
                    string _idEstado,
                    string _id_mov_cta_cte_pro,
                    string _interes)
	{
        try { 
        idCuota = Convert.ToInt32(_id_cuotas);
        numCuota = Convert.ToInt32(_num_cuotas);
        cantCuota = Convert.ToInt32(_cant_cuotas);
        importe = float.Parse(_importe);
        saldo = float.Parse(_saldo);
        fechaVencimiento = Convert.ToDateTime(_fecha_vecimiento);
        idFormaDePago = Convert.ToInt32(_id_forma_pago);
        idMovCtaCtePro = Convert.ToInt32(_id_mov_cta_cte_pro);
        idEstado = Convert.ToInt32(_idEstado);
        interes = Convert.ToInt32(_interes);

    }catch(Exception error){

    }
	}

    //Tuplas de Cuotas
    #region propiedades cuotas
    /// <summary>
    /// ID de la cuota
    /// </summary>
    private int idCuota;
    public int IdCuota
    {
        get { return idCuota; }
    }

    /// <summary>
    /// Numero dela cuota
    /// </summary>
    private int numCuota;
    public int NumCuota
    {
        set { numCuota = value; }
        get { return numCuota; }
    }

    /// <summary>
    /// Cantidad de Cuotas
    /// </summary>
    private int cantCuota;
    public int CantCuota
    {
        set { cantCuota = value; }
        get { return cantCuota; }
    }

    /// <summary>
    /// Importe de la Cuota
    /// </summary>
    private float importe;
    public float Importe
    {
        set { importe = value; }
        get { return importe; }
    }

    /// <summary>
    /// Saldo de la cuota
    /// </summary>
    private float saldo;
    public float Saldo
    {
        set { saldo = value; }
        get { return saldo; }
    }

    /// <summary>
    /// Fecha de vencimiento
    /// </summary>
    private DateTime fechaVencimiento;
    public DateTime FechaVencimiento
    {
        set { fechaVencimiento = value; }
        get { return fechaVencimiento; }
    }

    /// <summary>
    /// Id de la forma de pago
    /// </summary>
    private int idFormaDePago;
    public int IdFormaDePago
    {
        set { idFormaDePago = value; }
        get { return idFormaDePago; }
    }

    /// <summary>
    /// Id de la forma de pago
    /// </summary>
    private int idEstado;
    public int IdEstado
    {
        set { idEstado = value; }
        get { return idEstado; }
    }

    /// <summary>
    /// ID del movimiento
    /// </summary>
    private int idMovCtaCtePro;
    public int IdMovCtaCtePro
    {
        set { idMovCtaCtePro = value; }
        get { return idMovCtaCtePro; }
    }

    private int interes;
    public int Interes
    {
        set { interes = value; }
        get { return interes; }
    }

    #endregion

    /// <summary>
    /// Guarda un registro
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
            SqlCommand procCuotas = new SqlCommand("sp_ab_cuotas", _conexion.getSqlConnection());
            procCuotas.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procCuotas.Parameters.Add(new SqlParameter("@opcion", opcion));
            procCuotas.Parameters.Add(new SqlParameter("@id_cuotas", IdCuota));
            procCuotas.Parameters.Add(new SqlParameter("@num_cuotas", NumCuota));
            procCuotas.Parameters.Add(new SqlParameter("@cant_cuotas", CantCuota));
            procCuotas.Parameters.Add(new SqlParameter("@importe", Importe));
            procCuotas.Parameters.Add(new SqlParameter("@saldo", Saldo));
            procCuotas.Parameters.Add(new SqlParameter("@interes", Interes));
            procCuotas.Parameters.Add(new SqlParameter("@fecha_vencimiento", FechaVencimiento));
            procCuotas.Parameters.Add(new SqlParameter("@id_forma_pago", IdFormaDePago));
            procCuotas.Parameters.Add(new SqlParameter("@id_estado", IdEstado));
            procCuotas.Parameters.Add(new SqlParameter("@id_mov_cta_cte_pro", IdMovCtaCtePro));
            // El procedimiento o la función 'sp_ab_cuotas' esperaba el parámetro '@id_forma_pago', que no se ha especificado
            //Ejecuto la consulta
            procCuotas.ExecuteNonQuery();
            return "OK";
        }
        catch (Exception exc)
        {
            return exc.ToString();//"ERROR";
        }
    }
}
