<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Compras.aspx.cs" Inherits="Compras" Title="PcSys | Compras" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/jquery.autocomplete.js"></script>

    <script type="text/javascript" language="javascript" src="../js/jquery.validate.js"></script>

    <script type="text/javascript" language="javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" language="javascript" src="../js/compras.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>

    <%--<script type='text/javascript' src='../js/compras.js'></script>--%>

    <script type="text/javascript">
    //Organiza los tabs
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab active');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    
    
    $(document).ready(function() {
    $("input[id*=tbNum]").numeric();
    $("input[id*=tbCant]").numeric();

    $("input[id*=tbValor]").css("display", "none");
    $("span[id*=lbValor]").css("display", "none");
    autocompleteBancos();
    //Se muestran solo los botones necesarios
    $("input[id*=imgbtNuevo]").css("display", "inline");
    $("input[id*=imgbtGuardar]").css("display", "none");
    $("input[id*=imgbtdMod]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
    $("input[id*=imgbtCancel]").css("display", "inline");
    
    $("#aspnetForm").validate({
                //definir reglas
                rules: {  
                    <%= tbProveedor.UniqueID %>: { required: true},
                    <%= tbNum.UniqueID %>: { required: true}
               
                },
                //mensajes a mostrar en caso que no se cumplan las reglas
                 messages: {
                   <%= tbProveedor.UniqueID %>: { required: "Campo Obligatorio"},
                   <%= tbNum.UniqueID %>: { required: "Campo Obligatorio"}
                  },
                
                //llamar a funcion guardar en caso que se hallan validados
                //los campos requeridos
	            submitHandler: function() {
	                guardarFactura();
	            } 
            }); //validador fin
            
    //control de checkbox condicion de pago
     $("#cbCredito").change(function(){

        if ($("#cbCredito").is(":checked")) {
          document.forms[0].cbContado.checked = false;
          $("#lbContado").removeClass("LabelSelected");
          $("#lbCredito").addClass("LabelSelected");
          $("#divCuotas").slideDown();
          $("#divPago").slideUp();
          autocompleteTarjeta();
          //divPago
        } else {
          document.forms[0].cbContado.checked = true;
          $("#lbContado").addClass("LabelSelected");
          $("#lbCredito").removeClass("LabelSelected");
          $("#divCuotas").slideUp();
          $("#divPago").slideDown();
         // autocompleteCheque();
        }    
      });
      
          //control de checkbox condicion de pago
     $("#cbContado").change(function(){

        if ($("#cbContado").is(":checked")) {
          document.forms[0].cbCredito.checked = false;
          $("#lbCredito").removeClass("LabelSelected");
          $("#lbContado").addClass("LabelSelected");
          $("#divCuotas").slideUp();
          $("#divPago").slideDown();
        } else {
          document.forms[0].cbCredito.checked = true;
          $("#lbCredito").addClass("LabelSelected");
          $("#lbContado").removeClass("LabelSelected");
          $("#divPago").slideDown();
        }    
      });
      
      
      
      
      
      
      
      $("#cbTargeta").change(function(){

        if ($("#cbTargeta").is(":checked")) {
          document.forms[0].cbCheque.checked = false;
          $("#lbCheque").removeClass("LabelSelected");
          $("#lbTargeta").addClass("LabelSelected");
          $("input[id*=tbValor]").css("display", "inline");
          $("span[id*=lbValor]").css("display", "inline");
          //$("#divCuotas").slideDown();
          //$("#divPago").slideUp();
          $("#divCheque").slideUp();
          autocompleteTarjeta();
          //divPago
        } else {
          document.forms[0].cbCheque.checked = true;
          $("#lbCheque").addClass("LabelSelected");
          $("#lbTargeta").removeClass("LabelSelected");
          $("#divCheque").slideDown();
        }    
      });
      
          //control de checkbox condicion de pago
     $("#cbCheque").change(function(){

        if ($("#cbCheque").is(":checked")) {
          document.forms[0].cbTargeta.checked = false;
          $("#lbTargeta").removeClass("LabelSelected");
          $("#lbCheque").addClass("LabelSelected");
          //autocompleteBancos();
          $("input[id*=tbValor]").css("display", "none");
          $("span[id*=lbValor]").css("display", "none");
          //////////////////////////////////////////////////////////////////////tbValor
          //$("#divCuotas").slideUp();
          $("#divCheque").slideDown();
        } else {
          document.forms[0].cbTargeta.checked = true;
          $("#lbTargeta").addClass("LabelSelected");
          $("#lbCheque").removeClass("LabelSelected");
          $("#divCheque").slideDown();
        }    
      });

      
          //control de checkbox forma de pago
     /*$("#cbTargeta").change(function(){
        if ($("#cbTargeta").is(":checked")) {
          document.forms[0].cbCheque.checked = false;
          $("#lbCheque").removeClass("LabelSelected");
          $("#lbTargeta").addClass("LabelSelected");
          autocompleteTarjeta();
          //divPago
        } else {
          document.forms[0].cbCheque.checked = true;
          $("#lbCheque").addClass("LabelSelected");
          $("#lbTargeta").removeClass("LabelSelected");
          autocompleteCheque();
        }    
      });
          //control de checkbox condicion de pago
     $("#cbCheque").change(function(){

        if ($("#cbCheque").is(":checked")) {
          document.forms[0].cbTargeta.checked = false;
          $("#lbTargeta").removeClass("LabelSelected");
          $("#ldCheque").addClass("LabelSelected");
          autocompleteCheque();
        } else {
          document.forms[0].cbTargeta.checked = true;
          $("#lbTargeta").addClass("LabelSelected");
          $("#ldCheque").removeClass("LabelSelected");
          autocompleteTarjeta();
        }    
      });*/
    });
//Inhabilita los campos del ABM
function habilitarCampos() {            
    $("input[id*=tbNombreCuenta]").removeAttr('disabled');
    $("select[id*=chPeriodo]").removeAttr('disabled');
}

//Habilita los campos del ABM
function inhabilitarCampos() {            
    $("input[id*=tbNombreCuenta]").removeAttr('disabled');
    $("input[id*=tbNombreCuenta]").attr('disabled', 'disabled'); 
    $("select[id*=chPeriodo]").removeAttr('disabled');
    $("select[id*=chPeriodo]").attr('disabled', 'disabled'); 
}

function nuevo(){
    habilitarCampos();
    $("input[id*=imgbtNuevo]").css("display", "none");
    $("input[id*=imgbtGuardar]").css("display", "inline");
    $("input[id*=imgbtdMod]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
    $("input[id*=imgbtCancel]").css("display", "inline");
    return false;
}

function cancela(){
    inhabilitarCampos();
    $("input[id*=imgbtNuevo]").css("display", "inline");
    $("input[id*=imgbtGuardar]").css("display", "none");
    $("input[id*=imgbtdMod]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
    $("input[id*=imgbtCancel]").css("display", "inline");
    return false;
}
 
 
    </script>

    <div style="display: block;" id="content_2" class="content">
        <table style="width: 100%">
            <tr>
                <td>
                    <fieldset id="basic">
                        <center>
                            <table id="Table1" width="100%" runat="server">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbNum" Text="Numero" runat="server" Font-Bold="True" ForeColor="Gray"></asp:Label></td>
                                    <td align="left">
                                        <asp:TextBox ID="tbNum" runat="server" MaxLength="9" Width="70px"></asp:TextBox><font
                                            color="red">*</font></td>
                                    <td align="right">
                                        <asp:Label ID="lbFecha" Text="Fecha" runat="server" Font-Bold="True" ForeColor="Gray"></asp:Label></td>
                                    <td align="left">
                                        <asp:TextBox ID="tbFecha" runat="server" Width="70px"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:Label ID="lbCondPago" Text="Condicion de Pago:" Font-Bold="True" runat="server"
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td>
                                        <!--Contado -->
                                        <input id="cbContado" type="checkbox" checked="checked" />
                                        <label id="lbContado" for="cbContado">
                                            Contado
                                        </label>
                                    </td>
                                    <td>
                                        <!--Credito -->
                                        <input id="cbCredito" type="checkbox" />
                                        <label id="lbCredito" for="cbCredito">
                                            Credito</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 23px">
                                        <asp:Label ID="lbProveedor" Text="Proveedor" runat="server" Font-Bold="True" ForeColor="Gray"></asp:Label>
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:TextBox ID="tbProveedor" runat="server" Width="500px"></asp:TextBox><font color="red">*</font>
                                    </td>
                                    <td align="right" colspan="2">
                                        <asp:Label ID="lbDoc" Text="Documento" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="tbDoc" Width="122px" runat="server"></asp:TextBox><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 23px">
                                        <asp:Label ID="lbDireccion" Text="Direccion" runat="server" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td align="left" colspan="8">
                                        <asp:TextBox ID="tbDireccion" Width="420px" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="9">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="12">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 69px" align="right">
                                                    <asp:Label ID="lbComponente" runat="server" Text="Componente" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td align="left" style="width: 133px">
                                                    <asp:DropDownList ID="chComponente" runat="server" DataSourceID="dsComponente" DataTextField="descripcion"
                                                        DataValueField="id_materia_prima">
                                                    </asp:DropDownList><asp:SqlDataSource ID="dsComponente" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                                                        SelectCommand="SELECT [id_materia_prima], [descripcion] FROM [PCCC_MATERIA_PRIMA]">
                                                    </asp:SqlDataSource>
                                                </td>
                                                <td style="width: 66px" align="right">
                                                    <asp:Label ID="lbCant" Text="Cantidad" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 22px" align="left">
                                                    <asp:TextBox ID="tbCant" MaxLength="3" runat="server" Width="21px"></asp:TextBox>
                                                </td>
                                                <td style="width: 4px" colspan="5">
                                                    <table style="width: 184px">
                                                        <tr>
                                                            <td style="width: 71px">
                                                                <asp:Button ID="btAgregar" Text="Agregar" OnClientClick="fnAddRow(); return false;"
                                                                    runat="server" Width="68px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btEditar" Text="Editar" OnClientClick="editarDetalle(); return false;"
                                                                    runat="server" Width="68px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btGuardar" Text="Guardar" OnClientClick="guardarDetalle(); return false;"
                                                                    runat="server" Width="68px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btEliminar" Text="Eiminar" runat="server" Width="68px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btCancelar" Text="Cancelar" OnClientClick="cancelarDetalle(); return false;"
                                                                    runat="server" Width="68px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="12">
                                                    <table cellpadding="0" width="100%" cellspacing="0" border="1" class="display" id="tablaDetalle">
                                                        <tbody>
                                                            <!-- Aqui se carga la tabla dinamica-->
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <div id="divCuotas">
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td colspan="8" style="padding-top: 10px;">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 169px;">
                                                        <asp:Label ID="lbCantCuotas" Text="Cantidad de Cuotas" runat="server" Width="129px"
                                                            Font-Bold="True"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="tbCantCuotas" MaxLength="2" runat="server" Width="14px"></asp:TextBox>
                                                        <font color="red">*</font>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lbFormaPago" runat="server" Text="Forma de Pago"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="chFormaPago" runat="server" DataSourceID="dsFormaPago" DataTextField="cant_dias"
                                                            DataValueField="id_forma_pago">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbDias" Text="Dias" runat="server"></asp:Label>
                                                        <asp:SqlDataSource ID="dsFormaPago" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                                                            SelectCommand="SELECT [id_forma_pago], [cant_dias] FROM [PCCC_FORMA_DE_PAGO]"></asp:SqlDataSource>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lbInteres" runat="server" Text="Interes del"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="chInteres" runat="server" DataSourceID="dsInteres" DataTextField="porcentaje" DataValueField="id_interes">
                                                        </asp:DropDownList><asp:SqlDataSource ID="dsInteres" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                                                            SelectCommand="SELECT [id_interes], [porcentaje] FROM [PCCC_INTERES]"></asp:SqlDataSource>
                                                        <asp:Label ID="porcent" Text="%" runat="server"></asp:Label>
                                                 
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <div id="divPago">
                                            <table align="center" width="100%">
                                                <tr>
                                                    <td colspan="8" style="padding-top: 10px;">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 169px">
                                                        &nbsp; &nbsp; &nbsp; &nbsp;
                                                        <asp:Label ID="Label4" Text="Condicion de Pago:" Font-Bold="True" runat="server"
                                                            Width="110px"></asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <!--Contado -->
                                                        <input id="cbCheque" type="checkbox" checked="checked" />
                                                        <label id="lbCheque" for="cbCheque">
                                                            Cheque
                                                        </label>
                                                        <%--</td>
                                    <td>--%>
                                                        <!--Credito -->
                                                        <input id="cbTargeta" type="checkbox" />
                                                        <label id="lbTargeta" for="cbTargeta">
                                                            Targeta</label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbValor" runat="server" Text="Num del Valor"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbValor" runat="server" Width="350px"></asp:TextBox>
                                                        <!-- TARJETA -->
                                                        <div id="divCheque">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lbNroCheque" runat="server" Text="Nro Cheque"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbNroCheque" runat="server"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lbBanco" Text="Banco" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbBanco" runat="server"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <table align="center" width="100%">
                                            <tr>
                                                <td colspan="8" style="padding-top: 10px;">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 170px;">
                                                    <asp:Label ID="lbTotal" Text="Total Factura" runat="server" Font-Bold="True" Width="86px"></asp:Label>&nbsp;
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="tbTotal" runat="server"></asp:TextBox></td>
                                                <td width="50%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 23px">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </center>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <!--BOTONERA-->
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgbtNuevo" runat="server" OnClientClick="nuevaFactura(); return false;"
                                        ImageUrl="../images/new.ico" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtGuardar" runat="server" OnClientClick="guardarFactura(); return false"
                                        ImageUrl="../images/save.png" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtdMod" runat="server" ImageUrl="../images/list.ico" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtBorrar" runat="server" OnClientClick="borrar(); return false;"
                                        ImageUrl="../images/delete.ico" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtCancel" runat="server" OnClientClick="cancelarFatura(); return false;"
                                        ImageUrl="../images/cancel.png" />
                                </td>
                            </tr>
                        </table>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <!-- Tabla Dinamica -->
                    <div id="divContenedor">
                        <!-- tabla donde se genera la tabla dinamica-->
                        <table cellpadding="0" cellspacing="0" border="1" class="display" id="tablaFactura">
                            <tbody>
                                <!-- Aqui se carga la tabla dinamica-->
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <!--PopUp-->
        <!-- Elimina a una persona -->
        <div style="display: none;">
            <div id="anularFactura" title="Anular Factura">
                <!-- Mensaje de Confirmacion -->
                Esta seguro que desea anular la factura seleccionado?
            </div>
        </div>
    </div>
</asp:Content>
