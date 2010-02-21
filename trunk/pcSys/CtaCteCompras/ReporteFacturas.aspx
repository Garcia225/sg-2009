<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ReporteFacturas.aspx.cs" Inherits="CtaCteCompras_ReporteFacturas" Title="PcSys | Reportes Facturas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" language="javascript" src="../js/reporteFactura.js"></script>--%>

    <script type="text/javascript" src="../js/jquery.autocomplete.js"></script>

    <script type="text/javascript" language="javascript" src="../js/jquery.validate.js"></script>

    <script type="text/javascript" language="javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" language="javascript" src="../js/reporteFactura.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>

    <script type="text/javascript">
    //Organiza los tabs
    $("input[id*=tbFechaInicio]").mask("99/99/9999");
    $("input[id*=tbFechaFin]").mask("99/99/9999");
    
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab active');
    
    </script>

    <div style="display: block;" id="content_2" class="content">
        <!-- Titulo de la pagina -->
        <center>
        <div id="divTitulo" class="titulo">
            <strong><span style="font-size: 14pt">
            Listado de Facturas </span></strong>
        </div>
        </center>
        <br />
        <br />
        <!-- Tabla que contiene la Barra de busqueda -->
        <table id="tblOpciones" class="botonera" align="center">
        <tr>
        <td>
        <asp:Label id="chFiltrado" Text="Filtrar por:" runat="server"></asp:Label>
        </td>
        <td>
        <asp:DropDownList ID="chOpcion" runat="server">
        <asp:ListItem Value="1" Text="Todos"></asp:ListItem>
        <asp:ListItem Value="2" Text="Por proveedor"></asp:ListItem>
        </asp:DropDownList>
        </td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="lbProveedor" Text="Proveedor" runat="server"></asp:Label>
                </td>
                <td>
                <asp:DropDownList ID="chProveedores" runat="server" DataSourceID="dsProveedores" DataTextField="Nombre" DataValueField="id_proveedor">
                
                </asp:DropDownList><asp:SqlDataSource ID="dsProveedores" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                    SelectCommand="SELECT     id_proveedor, razon_social + ' -  ' + apellido AS Nombre&#13;&#10;FROM         PCCC_PROVEEDORES&#13;&#10;WHERE     (borrado = 'N')">
                
                </asp:SqlDataSource>
                </td>
                <%--<td>
                    <asp:TextBox ID="tbProveedor" runat="server" Width="500px"></asp:TextBox>
                    <asp:TextBox ID="tbID" runat="server" Width="16px" ></asp:TextBox>
                </td>--%>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbEntre" Text="Entre fechas:" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbFechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbFechaInicio" runat="server" Width="70px"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lbFechaFin" runat="server" Text="Fecha Fin"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbFechaFin" runat="server" Width="70px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <!-- Boton buscar, utilizado para actualizar el reporte -->
                    <asp:ImageButton ID="btnListar" runat="server" ToolTip="Listar" ImageUrl="~/images/list.ico" OnClick="btnListar_Click" />
                    <asp:ImageButton ID="imgbtnCancelar" runat="server" ToolTip="Salir" ImageUrl ="~/images/cancel.png"  OnClick="imgbtnCancelar_Click" />
                </td>
            </tr>
        </table>
        <hr />
        <!-- Area del reporte -->
        <div id="zona Reporte">
            <!-- Utilizamos una tabla para centrar el reporte -->
            <table align="center" width="100%">
                <tr>
                    <td>
                        <center>
                            <CR:CrystalReportViewer ID="crystalReportViewer" runat="server" AutoDataBind="true"
                                DisplayPage="true" DisplayGroupTree="False" BorderStyle="Inset" HasToggleGroupTreeButton="False"
                                HasSearchButton="True" Visible="true" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" />
                        </center>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
