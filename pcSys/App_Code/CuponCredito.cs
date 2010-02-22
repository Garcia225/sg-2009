using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;
using log4net;

/// <summary>
/// Summary description for CuponCredito
/// </summary>
public class CuponCredito
{
	public CuponCredito(string idCuponCredito,
                        string idTargetaCredito,
                        string idFactura,
                        string _monto)
	{
        id_cupon_credito = Convert.ToInt32(idCuponCredito);
        id_targeta_credito = Convert.ToInt32(idTargetaCredito);
        id_factura = Convert.ToInt32(idFactura);
        monto = float.Parse(_monto);
	}

    #region propiedades cupon credito

    /// <summary>
    /// ID del cupon de credito
    /// </summary>
    private int id_cupon_credito;
    public int Id_cupon_credito
    {
        set { id_cupon_credito = value; }
        get { return id_cupon_credito; }
    }

    /// <summary>
    /// ID de targeta de credito
    /// </summary>
    private int id_targeta_credito;
    public int Id_targeta_credito
    {
        set { id_targeta_credito = value; }
        get { return id_targeta_credito; }
    }

    /// <summary>
    /// ID de targeta de credito
    /// </summary>
    private int id_factura;
    public int Id_factura
    {
        set { id_factura = value; }
        get { return id_factura; }
    }

    /// <summary>
    /// Monto pagado con la targeta
    /// </summary>
    private float monto;
    public float Monto
    {
        set { monto = value; }
        get { return monto; }
    }

    #endregion

    /// <summary>
    /// Guarda un nuevo registro
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
            SqlCommand procCheques = new SqlCommand("sp_a_cupon_credito", _conexion.getSqlConnection());
            procCheques.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procCheques.Parameters.Add(new SqlParameter("@id_targeta", Id_targeta_credito));
            procCheques.Parameters.Add(new SqlParameter("@id_factura", Id_factura));
            procCheques.Parameters.Add(new SqlParameter("@monto", Monto));
            //Ejecuto la consulta
            procCheques.ExecuteNonQuery();


            return "OK";
        }
        catch (Exception exc)
        {
            return exc.ToString();//"ERROR";
        }
    }
}
