using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for Cheques
/// </summary>
public class Cheques
{
	public Cheques( string _importe,
                    string _num_cheque,
                    string _id_banco)
	{
        importe = float.Parse(_importe);
        num_cheque = Convert.ToInt32(_num_cheque);
        id_banco = Convert.ToInt32(_id_banco);
	}


    #region propiedades cheques
    /// <summary>
    /// ID de la cuota
    /// </summary>
    private float importe;
    public float Importe
    {
        set { importe = value; }
        get { return importe; }
    }

    /// <summary>
    /// Numero dela cuota
    /// </summary>
    private int num_cheque;
    public int Num_cheque
    {
        set { num_cheque = value; }
        get { return num_cheque; }
    }

    /// <summary>
    /// Importe de la Cuota
    /// </summary>
    private int id_banco;
    public int Id_banco
    {
        set { id_banco = value; }
        get { return id_banco; }
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
            SqlCommand procCheques = new SqlCommand("sp_ab_cheque", _conexion.getSqlConnection());
            procCheques.CommandType = CommandType.StoredProcedure;

            //pasa los parametros al procedimiento almacenado
            //La opcion 1 es para guardar nuevo cliente
            procCheques.Parameters.Add(new SqlParameter("@opcion", opcion));
            procCheques.Parameters.Add(new SqlParameter("@id_cheque", Id_banco));
            procCheques.Parameters.Add(new SqlParameter("@importe", Importe));
            procCheques.Parameters.Add(new SqlParameter("@num_cheque", Num_cheque));
            procCheques.Parameters.Add(new SqlParameter("@id_banco", Id_banco));
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
