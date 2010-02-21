using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Enterprise;
using CrystalDecisions.Shared;
using CrystalDecisions;
using CrystalDecisions.Web.Design;
using CrystalDecisions.Web;
using Microsoft.Reporting.WebForms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using AjaxPro;
public partial class CtaCteCompras_ReporteFacturas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnListarClick(object sender, ImageClickEventArgs e)
    {
        //mostrarInforme();
        //txtFiltro.Text = "";
    }

    /*    [System.Web.Services.WebMethod()]
    public static string getPersonas()*/
   // [System.Web.Services.WebMethod()]
    protected void mostrarInforme()
    //public static string mostrarInforme(string opcion, string filtro, string fechaIni, string fechaLast)
    {
        // Crear la conexión a la base de datos
        Conexion conexion = new Conexion();
        //string filter = txtFiltro.Text;
        // La opción de filtrado esta establecida por el combo ddlFiltrarPor
        //string opcion = "" + ddlFiltrarPor.SelectedIndex;
        string opcion = "1";
        //string filtro = ""+ tbID.Text;
        string filtro = "" + chProveedores.SelectedValue;
        string fechaIni = ""+ tbFechaInicio.Text;
        string fechaLast = ""+ tbFechaFin.Text;

        try
        {
            if(filtro == ""){
                opcion = "2";
                filtro = "1";
            }

            // Abrir la conexión a la base de datos
            conexion.OpenConnection();

            // El adaptador sql
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            // El dataset para el inventario
            DataSet dataSet = new DataSet();
            SqlCommand sqlCommand = new SqlCommand("sp_reporte_factura", conexion.getSqlConnection());
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@opcion", Convert.ToInt32(opcion));
            sqlCommand.Parameters.AddWithValue("@filtro", Convert.ToInt32(filtro));
            sqlCommand.Parameters.AddWithValue("@fecha_inicio", Convert.ToDateTime(fechaIni));
            sqlCommand.Parameters.AddWithValue("@fecha_fin", Convert.ToDateTime(fechaLast));

            sqlCommand.ExecuteNonQuery();
            // Creo el documento del reporte
            ReportDocument reportDocument = new ReportDocument();
            string rptPath = "~\\Reportes\\reporteFactura.rpt";
            // Path del reporte a utilizar
            string reportPath = Server.MapPath(rptPath);

            // Cargo el dataset con los datos a visualizar
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataSet, "sp_reporte_factura");

            // Cargo el reporte al report document
            reportDocument.Load(reportPath);
            // Seteo los datos al report document
            reportDocument.SetDataSource(dataSet);

            // Cargo el nuevo documento al visor de reporte y lo vuelvo a mostrar.
            crystalReportViewer.ReportSource = reportDocument;

            /*odsCliente.SelectParameters.Add("opcion", opcion);
            odsCliente.SelectParameters.Add("filtro", filter);
            */
            // Setear aquí el nombre de usuario
            /* SetReportParamValue(crystalReportViewer, "opcion", _nombreUsuario);
             SetReportParamValue(crystalReportViewer, "filtro", numCaja.ToString());
             */
            crystalReportViewer.Visible = true;
            crystalReportViewer.DataBind();
        }

        finally
        {
            // Cerrar la conexión
            conexion.CloseConnection();
        }
    }
    protected void btnListar_Click(object sender, ImageClickEventArgs e)
    {
        mostrarInforme();
    }

    protected void imgbtnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CtaCteCompras/ReportesPcSys.aspx");
    }
}
