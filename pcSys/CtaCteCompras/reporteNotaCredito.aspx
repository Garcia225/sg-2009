<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="reporteNotaCredito.aspx.cs" Inherits="CtaCteCompras_Reporte_Nota_Credito"
    Title="PcSys | Listado de NotaCredito " %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            Listado de Notas de Creditos </span></strong>
        </div>
        </center>
    <br />
    <br />
    <!-- Tabla que contiene la Barra de busqueda -->
    <table id="tblOpciones" class="botonera" align="center">
        <tr>
            <td align="right">
                Filtrar por:
            </td>
            <!-- Combo que se utiliza para las opciones de filtrado -->
            <td>
                <asp:DropDownList ID="ddlFiltrarPor" runat="server" Width="160px" AutoPostBack="True">
                    <asp:ListItem>Proveedor</asp:ListItem>
                    <asp:ListItem>Fecha</asp:ListItem>
                    <asp:ListItem>Numero Nota Credito</asp:ListItem>
                    <asp:ListItem>Numero de Factura</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right">
                &nbsp;&nbsp;&nbsp; Filtro:
            </td>
            <!-- Filtro para el informe -->
            <td>
                <asp:TextBox ID="txtFiltro" Width="220" MaxLength="25" runat="server" />
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
                <asp:ImageButton ID="btnListar" runat="server" ToolTip="Listar" ImageUrl ="~/images/list.ico" OnClick="btnListarClick"/>
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
                        DisplayPage="true" DisplayGroupTree="False" BorderStyle="Inset"
                        HasToggleGroupTreeButton="False" HasSearchButton="True" Visible="true" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" />
              
                       </center>
                </td>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>
