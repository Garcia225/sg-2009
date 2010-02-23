<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReportesPcSys.aspx.cs" Inherits="CtaCteCompras_ReportesPcSys" Title="PcSys | Reportes Generales " %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Reportes" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/notaCredito.js"></script>

    <script type="text/javascript">
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab active');
    </script>

    <!-- Inicio de la tabla , aqui se tendra el reporte que se cargara segun las opciones
que elija el usuario, su fuente de datos es cliente.xsd que el cual usa un procedimiento 
almacenado para hacer sus consultas-->
    <div style="display: block;" id="content_2" class="content">
       <!-- Titulo de la pagina -->
        <center>
        <div id="divTitulo" class="titulo">
            <strong><span style="font-size: 14pt">
            Reportes Generales </span></strong>
        </div>
        </center>
        <table width="100%">
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Reporte Proveedores" Target="_blank"
                        NavigateUrl="~/CtaCteCompras/reporteProveedores.aspx">
                    </asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink2" runat="server" Text="Reporte Nota Credito" Target="_blank"
                        NavigateUrl="~/CtaCteCompras/reporteNotaCredito.aspx">
                    </asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink3" runat="server" Text="Reporte Factura" Target="_blank"
                        NavigateUrl="~/CtaCteCompras/reporteFacturas.aspx">
                    </asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLink4" runat="server" Text="Reporte Cta Cte Proveedor" Target="_blank"
                        NavigateUrl="~/CtaCteCompras/reporteCtaCteProveedores.aspx">
                    </asp:HyperLink>
                </td>
            </tr>
        </table>
        <br />
        <hr />
        <!-- Area del reporte -->
        <div id="zona Reporte">
            <!-- Utilizamos una tabla para centrar el reporte -->
        </div>
    </div>
</asp:Content>
