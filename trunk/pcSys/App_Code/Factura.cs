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
/// Summary description for Factura
/// </summary>
public class Factura
{
    //private static readonly ILog _log = LogManager.GetLogger(typeof(Empleado));
	public Factura( string id_factura,
                    string id_proveedor,
                    string num_factura,
                    string _fecha,
                    string total_factura,
                    string id_condicion_de_pago,
                    string id_empleado,
                    string detalle_factura,
                    string _cantCuotas,
                    string sumaResta,
                    string estado)
	{
        idFactura= Convert.ToInt32(id_factura);
        idProveedor = Convert.ToInt32(id_proveedor);
        fecha = Convert.ToDateTime(_fecha);
        totalFactura = float.Parse(total_factura);
        empleado = Convert.ToInt32(id_empleado);
        condicion = Convert.ToInt32(id_condicion_de_pago);
        detalle = detalle_factura;
        numFactura = Convert.ToInt32(num_factura);
        cantCuotas = Convert.ToInt32(_cantCuotas);
        suma_resta = sumaResta;
        idEstado = Convert.ToInt32(estado);
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
    private int condicion;
    public int Condicion
    {
        set { condicion = value; }
        get { return condicion; }
    }

    //DETALLE
    /// <summary>
    /// Detalle de la factura
    /// </summary>
    private string detalle;
    public string Detalle {

        set { detalle = value; }
        get { return detalle; }
    
    }

    /// <summary>
    /// Cantidad de cuotas con que se pagara la 
    /// factura en caso de que sea credito
    /// </summary>
    private int cantCuotas;
    public int CantCuotas
    {

        set { cantCuotas = value; }
        get { return cantCuotas; }

    }

    /// <summary>
    /// Suma y resta
    /// </summary>
    private string suma_resta;
    public string SumaResta
    {

        set { suma_resta = value; }
        get { return suma_resta; }

    }

    /// <summary>
    /// Devuelve el id del estado
    /// </summary>
    private int idEstado;
    public int IdEstado
    {

        set { idEstado = value; }
        get { return idEstado; }

    }
    #endregion

    /// <summary>
    /// Trae todas las facturas registradas
    /// </summary>
    /// <returns></returns>
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

            //CAMBIARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
            
            //sentencia.Append(" select numFac, razonSocial, totalFac, fecha, id_proveedor, id_usuario  from viewFatura");
            sentencia.Append(" select cod, numFac, razonSocial, totalFac, fecha, contado, id_proveedor from viewFatura");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());
            //llena el dataset
            comando.Fill(dataSet, "viewFatura");


            //serializar dataset
            sb.Append(serial.JSON(dataSet.Tables["viewFatura"]));
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
    /// Guarda una factura con sus detalles
    /// </summary>
    /// <returns></returns>
    public string Guardar()
    {
        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();

            // Opcion a realizarce dentro del procedimiento almacenado
            int opcion = 1;

            //procedimiento cabecera
            SqlCommand proc = new SqlCommand("sp_ab_factura", _conexion.getSqlConnection());
            //SqlCommand procDetalle = new SqlCommand("sp_am_detalle_factura", _conexion.getSqlConnection());

            proc.CommandType = CommandType.StoredProcedure;
            //procDetalle.CommandType = CommandType.StoredProcedure;

            //cabecera   
            //pasar los parametros al procedimiento almacenado
            proc.Parameters.Add(new SqlParameter("@opcion", opcion));//1
            proc.Parameters.Add(new SqlParameter("@id_proveedor", "" + IdProvedor));//1
            proc.Parameters.Add(new SqlParameter("@num_factura", "" + NumFactura));//1
            proc.Parameters.Add(new SqlParameter("@id_factura", IdFactura));//1
            proc.Parameters.Add(new SqlParameter("@fecha", Fecha));//24/11/2009 0:00:00
            proc.Parameters.Add(new SqlParameter("@total_factura", ToralFactura));//120000.0
            proc.Parameters.Add(new SqlParameter("@id_condicion_de_pago", Condicion));//1
            proc.Parameters.Add(new SqlParameter("@id_empleado", Empleado));//1
            proc.Parameters.Add(new SqlParameter("@id_estado", IdEstado));

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
            // La función o el procedimiento sp_am_detalle_factura tiene demasiados argumentos.

            //serliazador
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            // creo el arrayList Principal, que contiene las filas a ser insertada
            ArrayList arrayListPrincipal = new ArrayList();

            //array list que contiene los valores a ser insertados
            ArrayList arrayListFilas = new ArrayList();

            // Deseralizar arreglo y crearlo en el arrayList Principal
            arrayListPrincipal = serializer.Deserialize<ArrayList>(Detalle);//"\"[[\"2\",1,\"12000\",\"24000\"]]\""
            //"[[\"29\",10,\"Pago Parcial de la cuota Nº 1/2 de la factura 321\"],[\"30\",10,\"Pago Parcial de la cuota Nº 2/2 de la factura 321\"]]"
            // x = arrayListPrincipal.Count;
            //recorrer arrayList Principal e insertar el detalle
            foreach (ArrayList arrayFila in arrayListPrincipal)
            {//[['2,'1,'12000,'24000']]
            // cant, codComp, costo, total
                /*SqlCommand procDetalle = new SqlCommand("sp_abm_factura_det", _conexion.getSqlConnection());


                procDetalle.CommandType = CommandType.StoredProcedure;*/
                SqlCommand procDetalle = new SqlCommand("sp_am_detalle_factura", _conexion.getSqlConnection());
                procDetalle.CommandType = CommandType.StoredProcedure;

                cont++;
                string cantidad = arrayFila[0].ToString();//"2"
                string codComp = arrayFila[1].ToString();//"1"
                string costoUnit = arrayFila[2].ToString();//"12000"
                string totalDetalle = arrayFila[3].ToString();//"24000"

                //pasa los parametros al procedimiento almacenado
                procDetalle.Parameters.Add(new SqlParameter("@opcion", opcion));
                procDetalle.Parameters.Add(new SqlParameter("@num_renglon", cont));//1
                procDetalle.Parameters.Add(new SqlParameter("@cantidad_recibida", '0'));
                procDetalle.Parameters.Add(new SqlParameter("@precio", costoUnit));//120000
                procDetalle.Parameters.Add(new SqlParameter("@cantidad", cantidad));//1
                procDetalle.Parameters.Add(new SqlParameter("@id_factura", getIdFac));//8
                procDetalle.Parameters.Add(new SqlParameter("@id_materia_prima", codComp));//4

                procDetalle.ExecuteNonQuery();

            }

            /////////////////////////////////////////
            //cta cte
            //Obtenemos el id de a cuenta del cliente
            string consultaIdCtaCte = "SELECT num_cta_cte_pro from PCCC_CTA_CTE_PROVEEDOR where id_proveedor = "+IdProvedor;
            //8
            SqlCommand consultaCtaCte = new SqlCommand(consultaIdCtaCte, _conexion.getSqlConnection());
            SqlDataReader readerCtaCte = consultaCtaCte.ExecuteReader();
            string idCtaCte = "";
            int getIdCtaCte = 0;
            while (readerCtaCte.Read())
            {
                idCtaCte = readerCtaCte[0].ToString();
            }
            reader.Close();
            getIdCtaCte = Convert.ToInt32(idCtaCte);
            /*SELECT num_cta_cte_pro from PCCC_CTA_CTE_PROVEEDOR where id_proveedor =*/

            /////////////////////////////////////////
            Proveedores prov = new Proveedores();
            string idCtaCtePro;
            //idCtaCtePro = prov.getIdCtaCte(IdProvedor);
            //Creo el movimieno en cta cte
            string fecha_ = fecha.ToString();
            string idFac_ = getIdFac.ToString();
            string cantCuotas_ = CantCuotas.ToString();
            MovCtaCte mov = new MovCtaCte("0", getIdCtaCte.ToString(), "0", SumaResta, 
                fecha_, idFac_, cantCuotas_);
            mov.Guardar();
            return "OK";

        }
        catch (Exception s) {
 
         return "ERROR";

        }
    }

    /// <summary>
    /// Anula la factura
    /// </summary>
    /// <returns></returns>
    public string Anular()
    {
        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();

            // Opcion a realizarce dentro del procedimiento almacenado
            int opcion = 3;

            //procedimiento cabecera
            SqlCommand proc = new SqlCommand("sp_ab_factura", _conexion.getSqlConnection());
            //SqlCommand procDetalle = new SqlCommand("sp_am_detalle_factura", _conexion.getSqlConnection());

            proc.CommandType = CommandType.StoredProcedure;
            //procDetalle.CommandType = CommandType.StoredProcedure;

            //cabecera   
            //pasar los parametros al procedimiento almacenado
            proc.Parameters.Add(new SqlParameter("@opcion", opcion));//1
            proc.Parameters.Add(new SqlParameter("@id_proveedor", "" + IdProvedor));//1
            proc.Parameters.Add(new SqlParameter("@num_factura", "" + NumFactura));//1
            proc.Parameters.Add(new SqlParameter("@id_factura", IdFactura));//1
            proc.Parameters.Add(new SqlParameter("@fecha", Fecha));//24/11/2009 0:00:00
            proc.Parameters.Add(new SqlParameter("@total_factura", ToralFactura));//120000.0
            proc.Parameters.Add(new SqlParameter("@id_condicion_de_pago", Condicion));//1
            proc.Parameters.Add(new SqlParameter("@id_empleado", Empleado));//1
            proc.Parameters.Add(new SqlParameter("@id_estado", IdEstado));

            proc.ExecuteNonQuery();

            return "OK";

        }
        catch (Exception s)
        {

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

            //sentencia.Append(" SELECT precio, descripcion from PCCC_MATERIA_PRIMA WHERE id_materia_prima = @id_materia_prima ");
            sentencia.Append("SELECT		mp.precio, mp.descripcion , iva.porcentaje ");
            sentencia.Append("from		PCCC_MATERIA_PRIMA as mp, PCCC_IVA as iva ");
            sentencia.Append("WHERE		mp.id_materia_prima = @id_materia_prima ");
            sentencia.Append("AND         mp.id_iva = iva.id_iva");


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
            _values.Add("descripcion", "" + datos[1]);
            _values.Add("porcentaje", "" + datos[2]);

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
    /// Obtiene las facturas vencidas de un proveedor
    /// </summary>
    /// <param name="fecha"></param>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string obtenerVencimiento(DateTime fecha, int idProveedor)
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

            sentencia.Append("Select num_doc, fecha, total_factura ");
            sentencia.Append("from PCCC_FACTURA where id_proveedor = @idProveedor and id_estado=1 ");
            sentencia.Append("and fecha = @fecha");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@idProveedor", idProveedor + "");
            comando.SelectCommand.Parameters.Add("@fecha", fecha + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_FACTURA");


            sb.Append(serial.JSON(dataSet.Tables["PCCC_FACTURA"]));

            // Obtener el registro
            //object[] datos = dataSet.Tables["PCCC_FACTURA"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            /*Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            string d = datos[0].ToString();
            _values.Add("costo", "" + datos[0]);
            */
            // Serializador de JavaScript
            //JavaScriptSerializer ser = new JavaScriptSerializer();

            // Serializo los datos al formato json
            //sb.Append(ser.Serialize(_values));
            //string j = sb.ToString();
            //conexion.CloseConnection();
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
    /// Obtengo las facturas de un proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string getFacturas(int idProveedor)
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

            /*sentencia.Append("Select id_factura, num_factura, fecha, total_factura ");
            sentencia.Append("from viewFacturas where id_proveedor = @idProveedor and id_estado=1 ");
            */
            
            sentencia.Append("Select cod, numFac, fecha, totalFac ");
            sentencia.Append("from viewFatura where id_proveedor = @idProveedor and contado = 'N'");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@idProveedor", idProveedor + "");
            //comando.SelectCommand.Parameters.Add("@fecha", fecha + "");

            //llena el dataset
            comando.Fill(dataSet, "viewFacturas");


            sb.Append(serial.JSON(dataSet.Tables["viewFacturas"]));
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
    /// Obtiene las cuotas de la factura de un cliente
    /// </summary>
    /// <param name="idFactura"></param>
    /// <returns></returns>
    public string getCuotas(int idFactura)
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

            sentencia.Append("select cuotas, numCuotas, importe, saldo,interes, fechaVencimiento, idFactura, ctaCtePro ");
            //sentencia.Append("select cuotas, numCuotas, importe, saldo, fechaVencimiento, idFactura ");
            
            sentencia.Append("from viewCuotas where  idFactura = @idFactura");

            /*select ct.id_cuotas as cuotas, ct.num_cuotas as numCuotas, ct.importe as importe, ct.saldo as saldo, ct.fecha_vencimiento as fechaVencimiento, mov.id_factura as idFactura
            from  PCCC_CUOTAS AS ct, PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES AS mov
            where    ct.id_mov_cta_cte_pro = mov.id_mov_cta_cte_pro*/


            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@idFactura", idFactura + "");//13
            //comando.SelectCommand.Parameters.Add("@fecha", fecha + "");

            //llena el dataset
            comando.Fill(dataSet, "viewCuotas");


            sb.Append(serial.JSON(dataSet.Tables["viewCuotas"]));
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
    /// Obtiene el movimiento
    /// </summary>
    /// <param name="idCuota"></param>
    /// <returns></returns>
    public string ObtenerMovCtaCte(int idFactura)
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
            sentencia.Append(" FROM PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES where id_factura = @id_factura");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@id_factura", idFactura + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES");




            // Obtener el registro
            object[] datos = dataSet.Tables["PCCC_MOVIMIENTO_CTA_CTE_PROVEEDORES"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            _values.Add("movimiento", "" + datos[0]);

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
    /// 
    /// </summary>
    /// <param name="idComponente"></param>
    /// <returns></returns>
    public string obtenerNombreProducto(int idProducto)
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

            sentencia.Append(" SELECT descripcion from PCCC_MATERIA_PRIMA WHERE id_materia_prima = @id_materia_prima");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@id_materia_prima", idProducto + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_MATERIA_PRIMA");

            // Obtener el registro
            object[] datos = dataSet.Tables["PCCC_MATERIA_PRIMA"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            string d = datos[0].ToString();
            _values.Add("nombre", "" + datos[0]);

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
    /// Obtengo las facturas de un proveedor
    /// </summary>
    /// <param name="idProveedor"></param>
    /// <returns></returns>
    public string obtenerFactura(int idProveedor)
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
            //SELECT PCCC_FACTURA.num_factura
            //FROM   PCCC_FACTURA INNER JOIN
            //       PCCC_PROVEEDORES ON PCCC_FACTURA.id_proveedor = PCCC_PROVEEDORES.id_proveedor
            //WHERE (PCCC_FACTURA.id_estado = 1)
            sentencia.Append("Select distinct num_factura, id_factura ");
            sentencia.Append("from PCCC_FACTURA, ");
            sentencia.Append("PCCC_PROVEEDORES where PCCC_FACTURA.id_proveedor = @idProveedor ");
            sentencia.Append("and PCCC_FACTURA.id_estado=1");

            //carga el data set

            comando = new SqlDataAdapter(sentencia.ToString(), conexion.getConectionString());

            comando.SelectCommand.Parameters.Add("@idProveedor", idProveedor + "");

            //llena el dataset
            comando.Fill(dataSet, "PCCC_FACTURA");




            //sb.Append(serial.JSON(dataSet.Tables["PCCC_FACTURA"]));


            // Obtener el registro
            //object[] datos = dataSet.Tables["PCCC_FACTURA"].Rows[0].ItemArray;


            // Crea el diccionario de datos y agrega todas las entradas
            //Dictionary<string, string> _values = new Dictionary<string, string>();
            //Falta modificar
            //string d = datos[0].ToString();
            //_values.Add("num_factura", "" + datos[0]);

            // Serializador de JavaScript
            //JavaScriptSerializer ser = new JavaScriptSerializer();

            // Serializo los datos al formato json
            //sb.Append(ser.Serialize(_values));
            sb.Append(serial.JSON(dataSet.Tables["PCCC_FACTURA"]));
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
    /// Retorna el uñtimo id de la fatura guardada
    /// </summary>
    /// <returns></returns>
    public string ultimoGuardado() {

        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();
            string consultaMax = "Select max(id_factura) from PCCC_FACTURA";

            SqlCommand consulta = new SqlCommand(consultaMax, _conexion.getSqlConnection());
            SqlDataReader reader = consulta.ExecuteReader();
            string numero = "";
            while (reader.Read())
            {
                numero = reader[0].ToString();
            }
            reader.Close();
            return numero;
        }catch (Exception exc){
            return "ERROR";
        }
    }

    /// <summary>
    /// Obtiene el id de un estdo determinado
    /// </summary>
    /// <param name="estado"></param>
    /// <returns></returns>
    public string getEstado(string estado)
    {

        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();
            string consultaEstado = "Select id_estado from PCCC_ESTADO where descripcion = '"+estado+"'";
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

    /// <summary>
    /// Obtiene el id de la condicion
    /// </summary>
    /// <param name="condicion"></param>
    /// <returns></returns>
    public string getCondicion(string condicion)
    {

        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();
            string consultaEstado = "Select id_condicion_de_pago from PCCC_CONDICION_DE_PAGO where contado = '" + condicion + "'";
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

    public void updateHaber(string monto, string id_Proveedor)
    {

        Conexion _conexion = null;
        int cont = 0;
        try
        {
            //falta codigo para guardar mov de la cta cte de la factura
            // Crear y abrir la conexión
            _conexion = new Conexion();
            _conexion.OpenConnection();
            string consultaEstado = "update PCCC_CTA_CTE_PROVEEDOR set haber = haber + "+monto+" where id_proveedor = "+id_Proveedor;
            // El nombre de columna 'PAGADO' no es válido
            SqlCommand consulta = new SqlCommand(consultaEstado, _conexion.getSqlConnection());
            consulta.ExecuteReader();
           
        }
        catch (Exception exc)
        {
            
        }
    }
}



