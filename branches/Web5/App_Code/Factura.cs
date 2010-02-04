using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for Factura
/// </summary>
public class Factura
{
	public Factura( string id_factura,
                    string id_proveedor,
                    string num_factura,
                    string _fecha,
                    string total_factura,
                    string id_condicion_de_pago,
                    string id_empleado,
                    string detalle_factura)
	{
        idFactura= Convert.ToInt32(id_factura);
        idProveedor = Convert.ToInt32(id_proveedor);
        fecha = Convert.ToDateTime(_fecha);
        totalFactura = float.Parse(total_factura);
        empleado = Convert.ToInt32(id_empleado);
        condicion = Convert.ToInt32(id_condicion_de_pago);
        detalle = detalle_factura;
        numFactura = Convert.ToInt32(num_factura);
        /*numRenglon = Convert.ToInt32(num_renglon);
        cantidad = Convert.ToInt32(_cantidad);
        idMateriaPrima = Convert.ToInt32(id_materia_prima);*/
	}

    public Factura() { }
    #region propiedades factura
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
    private int idFactura;
    public int IdFactura
    {
        get { return idFactura; }
    }

    /// <summary>
    /// Numero de la factura
    /// </summary>
    private int numFactura;
    public int NumFactura
    {
        set { numFactura = value; }
        get { return numFactura; }
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
    /// Total de la factura
    /// </summary>
    private float totalFactura;
    public float ToralFactura
    {
        set { totalFactura = value; }
        get { return totalFactura; }
    }

    /// <summary>
    /// Id de la condicion de pago
    /// </summary>
    private int condicionPago;
    public int CondicionPago
    {
        set { condicionPago = value; }
        get { return condicionPago; }
    }

    /// <summary>
    /// Id de la empleado
    /// </summary>
    private int empleado;
    public int Empleado
    {
        set { empleado = value; }
        get { return empleado; }
    }


    /// <summary>
    /// Precio
    /// </summary>
    private float condicion;
    public float Condicion
    {
        set { condicion = value; }
        get { return condicion; }
    }

    //DETALLE

    private string detalle;
    public string Detalle {

        set { detalle = value; }
        get { return detalle; }
    
    }



    #endregion



    /// <summary>
    /// Guarda una factura con sus detalles
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
            SqlCommand proc = new SqlCommand("sp_ab_factura", _conexion.getSqlConnection());
            SqlCommand procDetalle = new SqlCommand("sp_am_detalle_factura", _conexion.getSqlConnection());
            proc.CommandType = CommandType.StoredProcedure;


            //cabecera   
            //pasar los parametros al procedimiento almacenado
            proc.Parameters.Add(new SqlParameter("@opcion", opcion));
            proc.Parameters.Add(new SqlParameter("@id_proeedor", "" + IdProvedor));
            proc.Parameters.Add(new SqlParameter("@num_factura", "" + NumFactura));
            proc.Parameters.Add(new SqlParameter("@id_factura", IdFactura));
            proc.Parameters.Add(new SqlParameter("@fecha", Fecha));
            proc.Parameters.Add(new SqlParameter("@total_factura", ToralFactura));
            proc.Parameters.Add(new SqlParameter("@id_condicion_de_pago", Condicion));
            proc.Parameters.Add(new SqlParameter("@id_empleado", Empleado));



            proc.ExecuteNonQuery();

            //cta cte
            //Obtenemos el id de a cuenta del cliente
            string consultaMax = "Select max(id_factura) from PCCC_FACTURA";

            SqlCommand consulta = new SqlCommand(consultaMax, _conexion.getSqlConnection());
            SqlDataReader reader = consulta.ExecuteReader();
            string numero = "";
            int getIdFac = 0;
            while (reader.Read())
            {
                numero = reader[0].ToString();
            }
            reader.Close();
            getIdFac = Convert.ToInt32(numero);

            ////////////////////////////////////////////////////////////////////////////////////////////
            /*ZONA SERIALIZAR*/

            //serliazador
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // creo el arrayList Principal, que contiene las filas a ser insertada
            ArrayList arrayListPrincipal = new ArrayList();

            //array list que contiene los valores a ser insertados
            ArrayList arrayListFilas = new ArrayList();

            // Deseralizar arreglo y crearlo en el arrayList Principal
            arrayListPrincipal = serializer.Deserialize<ArrayList>(Detalle);

            // x = arrayListPrincipal.Count;
            //recorrer arrayList Principal e insertar el detalle
            foreach (ArrayList arrayFila in arrayListPrincipal)
            {//[['2,'1,'12000,'24000']]
            // cant, codComp, costo, total
                cont++;
                string cantidad = arrayFila[0].ToString();
                string codComp = arrayFila[1].ToString();
                string costoUnit = arrayFila[2].ToString();
                string totalDetalle = arrayFila[3].ToString();

                //pasa los parametros al procedimiento almacenado
                procDetalle.Parameters.Add(new SqlParameter("@opcion", opcion));
                procDetalle.Parameters.Add(new SqlParameter("@num_renglon", cont));
                procDetalle.Parameters.Add(new SqlParameter("@cantidad_recibida", '0'));
                procDetalle.Parameters.Add(new SqlParameter("@precio", costoUnit));
                procDetalle.Parameters.Add(new SqlParameter("@cantidad", cantidad));
                procDetalle.Parameters.Add(new SqlParameter("@id_factura", getIdFac));
                procDetalle.Parameters.Add(new SqlParameter("@id_materia_prima", codComp));

                procDetalle.ExecuteNonQuery();
                
            }
            return "OK";
        }
        catch (Exception s) { 
         return "ERROR";
        }
    }


    /// <summary>
    /// Devuelve el costo un componente
    /// </summary>
    /// <param name="idComponente">ID del componente</param>
    /// <returns></returns>
    public string obtenerCostoComponente(int idComponente) {
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

            sentencia.Append(" SELECT precio from PCCC_MATERIA_PRIMA WHERE id_materia_prima = @id_materia_prima");
           
            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@id_materia_prima", idComponente + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_MATERIA_PRIMA");

            // Obtener el registro
            object[] datos = dataSet.Tables["PCCC_MATERIA_PRIMA"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            string d = datos[0].ToString();
            _values.Add("costo", "" + datos[0]);

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
