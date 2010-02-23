<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="NotaCredito.aspx.cs" Inherits="NotadeCredito" Title="Nota Credito" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Nota_Credito" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" src="../js/jquery.autocomplete.js"></script>

    <%--<script type='text/javascript' src='../js/JSON.js'></script>--%>

    <script type="text/javascript" src="../js/jquery.validate.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>

    <script type="text/javascript" src="../js/notaCredito.js"></script>

    <script type="text/javascript">
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab active');
   
   $(document).ready(function() {  
   
            $("#aspnetForm").validate({
                //definir reglas
                rules: {  
                    <%= tbNumNotaCredito.UniqueID %>: { required: true},
                    <%= tbProveedor.UniqueID %>: { required: true},
                    <%= tbFactura.UniqueID %>: { required: true},
                    <%= tbMotivo.UniqueID %>: { required: true},
                    <%= tbTotal.UniqueID %>: { required: true}                
                },
                //mensajes a mostrar en caso que no se cumplan las reglas
                 messages: {
                   <%= tbNumNotaCredito.UniqueID %>: { required: "Campo Obligatorio"},
                    <%= tbProveedor.UniqueID %>: { required: "Campo Obligatorio"},
                    <%= tbFactura.UniqueID %>: { required: "Campo Obligatorio"},
                    <%= tbMotivo.UniqueID %>: { required: "Campo Obligatorio"},
                    <%= tbTotal.UniqueID %>: { required: "Campo Obligatorio"}
                  },
                
                //llamar a funcion guardar en caso que se hallan validados
                //los campos requeridos
	            submitHandler: function() {
	                guardar();
	            } 
            }); //validador fin
      });
      
      
      function cancela() {
// Llama a la función cancelar definica en el .js
    cancelar();
    // Oculta la validacion
    $("#aspnetForm").valid();
            
    return false;
} 
    </script>

    <div style="display: block;" id="content_3" class="content">
        <table width="100%">
            <tr>
                <td>
                    <fieldset id="basic">
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lbNumNotaCredito" runat="server" Text="N°"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbNumNotaCredito" runat="server" Width="42px" MaxLength="6"></asp:TextBox>
                                                <font color="red">*</font>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbFecha" runat="server" Text="Fecha:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFecha" runat="server" Width="70px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lbProveedor" runat="server" Text="Proveedor:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbProveedor" runat="server" Width="210"></asp:TextBox>
                                                <%-- <asp:DropDownList ID="chProveedor" runat="server" DataSourceID="dsProveedor" DataTextField="apellido"
                        DataValueField="id_proveedor">
                    </asp:DropDownList><asp:SqlDataSource ID="dsProveedor" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                        SelectCommand="SELECT [id_proveedor], [apellido] FROM [PCCC_PROVEEDORES]"></asp:SqlDataSource>--%>
                                                <font color="red">*</font>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbFactura" runat="server" Text="Factura:"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbFactura" runat="server" Width="105"></asp:TextBox>
                                                <font color="red">*</font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lbNumDoc" runat="server" Text="Numero de Documento"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbNumDoc" runat="server" Width="105"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbDireccion" runat="server" Text="Direccion"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="tbDireccion" runat="server" Width="350"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lbMotivo" runat="server" Text="Motivo:"></asp:Label>
                                            </td>
                                            <td colspan="4" align="left">
                                                <asp:TextBox ID="tbMotivo" runat="server" TextMode="MultiLine" Width="350px"></asp:TextBox>
                                                <font color="red">*</font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="3">
                                                <asp:Label ID="lbTotal" runat="server" Text="Total:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="tbTotal" runat="server" Width="63px" MaxLength="9"></asp:TextBox>
                                                <font color="red">*</font>
                                            </td>
                                        </tr>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgbtNuevo" runat="server" OnClientClick="nuevo(); return false;"
                                    ImageUrl="~/images/new.ico" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtGuardar" runat="server" ToolTip="Guardar" ImageUrl="~/images/save.png"
                                    OnClientClick="guardar(); " />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtModificar" runat="server" ToolTip="Modificar" ImageUrl="~/images/edit.png" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtBorrar" runat="server" ToolTip="Borrar" ImageUrl="~/images/delete.ico" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtCancelar" runat="server" ImageUrl="~/images/atras.ico"
                                    OnClientClick="cancela(); return false();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <table cellpadding="0" cellspacing="0" border="1" class="display" id="tablaNotaCredito">
                        <tbody>
                            <!-- Aqui se carga la tabla dinamica-->
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
        <!--PopUp-->
        <!-- Elimina a una nota de credito -->
        <div style="display: none;">
            <div id="eliminarNotaCredito" title="Eliminar Nota Credito">
                <!-- Mensaje de Confirmacion -->
                Esta seguro que desea eliminar la nota de credito seleccionada?
            </div>
        </div>
    </div>
</asp:Content>
