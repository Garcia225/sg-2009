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

public partial class CtaCteCompras_reporteCtaCteProveedores : System.Web.UI.Page
{
    /// <summary>
    /// Evento Page load de la p�gina
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Al cargar la pagina ya muestro el informe con todos los datos
        //actualizarInforme();
        mostrarInforme();
    }

    /// <summary>
    /// Evento del boton buscar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnListarClick(object sender, EventArgs e)
    {
        // Llama a la funcion que actualiza el informe
        //actualizarInforme();
        mostrarInforme();
        txtFiltro.Text = "";
    }

    /// <summary>
    /// Evento del boton salir
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalirClick(object sender, EventArgs e)
    {
        this.Response.Redirect("~/CtaCteCompras/Pagos.aspx", true);
    }

    /*
     
     PROBANDO...!!!!!!!!!!!!!!!!!!!!!!!!!!!
     
     */
    protected void mostrarInforme()
    {
        // Crear la conexi�n a la base de datos
        Conexion conexion = new Conexion();
        string filter = txtFiltro.Text;
        // La opci�n de filtrado esta establecida por el combo ddlFiltrarPor
        string opcion = "" + ddlFiltrarPor.SelectedIndex;

        try
        {

            // Abrir la conexi�n a la base de datos
            conexion.OpenConnection();

            // El adaptador sql
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            // El dataset para el inventario
            DataSet dataSet = new DataSet();
            SqlCommand sqlCommand = new SqlCommand("sp_reporte_ctacte_proveedores", conexion.getSqlConnection());
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@opcion", opcion);
            sqlCommand.Parameters.AddWithValue("@filtro", filter);

            sqlCommand.ExecuteNonQuery();
            // Creo el documento del reporte
            ReportDocument reportDocument = new ReportDocument();
            string rptPath = "~\\Reportes\\repCtaCteProv.rpt";
            // Path del reporte a utilizar
            string reportPath = Server.MapPath(rptPath);

            // Cargo el dataset con los datos a visualizar
            sqlDataAdapter.SelectCommand = sqlCommand;
            sqlDataAdapter.Fill(dataSet, "sp_reporte_ctacte_proveedores");

            // Cargo el reporte al report document
            reportDocument.Load(reportPath);
            // Seteo los datos al report document
            reportDocument.SetDataSource(dataSet);

            // Cargo el nuevo documento al visor de reporte y lo vuelvo a mostrar.
            crystalReportViewer.ReportSource = reportDocument;

            /*odsCliente.SelectParameters.Add("opcion", opcion);
            odsCliente.SelectParameters.Add("filtro", filter);
            */
            // Setear aqu� el nombre de usuario
            /* SetReportParamValue(crystalReportViewer, "opcion", _nombreUsuario);
             SetReportParamValue(crystalReportViewer, "filtro", numCaja.ToString());
             */
            crystalReportViewer.Visible = true;
            crystalReportViewer.DataBind();
        }

        finally
        {
            // Cerrar la conexi�n
            conexion.CloseConnection();
        }
    }

   
    /// <summary>
    /// evento del boton cancelar que redirecciona al Clientes.aspx
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgbtnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/CtaCteCompras/ReportesPcSys.aspx");
    }
}
