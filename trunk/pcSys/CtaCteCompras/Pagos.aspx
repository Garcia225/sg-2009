<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Pagos.aspx.cs" Inherits="CtaCteCompras_Pagos" Title="PcSys | Pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
//Organiza los tabs
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab active');
    document.getElementById('tab4').setAttribute('class', 'tab');
    </script>

    <script type="text/javascript" src="../js/jquery.autocomplete.js"></script>

    <%--<script type='text/javascript' src='../js/JSON.js'></script>--%>

    <script type="text/javascript" src="../js/jquery.validate.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>

    <script type='text/javascript' src='../js/pagos.js'></script>

    <div style="display: block;" id="content_3" class="content">
        <%-- <form id="form2" runat="server">--%>
        <table width="100%">
            <tr>
                <td align="right">
                    <asp:Label ID="lbProveedor" runat="server" Font-Bold="True" Text="Proveedor"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbProveedor" runat="server" Width="350px"></asp:TextBox><font
                                                color="red">*</font>
                </td>
                <td align="right">
                    <asp:Label ID="lbDoc" runat="server" Font-Bold="True" Text="Documento"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbDoc" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbDireccion" runat="server" Font-Bold="True" Text="Direccion"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbDireccion" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lbDebe" runat="server" Font-Bold="True" Text="Debe"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbDebe" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbHaber" runat="server" Font-Bold="True" Text="Haber"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbHaber" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Label ID="lbSaldo" runat="server" Font-Bold="True" Text="Saldo"></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="tbSaldo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lbCuotasVencidas" runat="server" Font-Bold="True" Text="Facturas Vencidas:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table cellpadding="0" width="100%" cellspacing="0" border="1" class="display" id="tablaVencimiento">
                        <tbody>
                            <!-- Aqui se carga la tabla dinamica-->
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Label ID="lbCuotasPendientes" runat="server" Font-Bold="True" Text="Facturas Pendientes:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table cellpadding="0" width="100%" cellspacing="0" border="1" class="display" id="tablaFacturas">
                        <tbody>
                            <!-- Aqui se carga la tabla dinamica-->
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        
        <div style="display: none;">
                <div id="alertMonto" title="Monto Mayor">
                    <!-- Mensaje de Confirmacion -->
                    El monto ingresado es mayor que el monto requerido
                </div>
            </div>
            
            <div style="display: none;">
                <div id="noNum" title="Numeros">
                    <!-- Mensaje de Confirmacion -->
                    Debe ingresar solo numeros al campo
                </div>
            </div>
            
            <div style="display: none;">
                <div id="cuotaPagada" title="Pagado">
                    <!-- Mensaje de Confirmacion -->
                    La cuota seleccionada ya esta saldada
                </div>
            </div>
        <%-- </form>--%>
    </div>
</asp:Content>
