<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Proveedor.aspx.cs" Inherits="Personas" Title="PcSyc | Periodos" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript" src="../js/jquery.simplemodal.js"></script>

    <script type="text/javascript" src="../js/jquery.simpletip-1.3.1.min.js"></script>

    <script type="text/javascript" src="../js/jqgrid/src/jqModal.js"></script>
    
    <script type="text/javascript" src="../js/jquery-ui-1.7.1.custom.min.js"></script>
    
    <script type="text/javascript" src="../js/jquery-ui-1.7.2.custom.min.js"></script>

    <script type="text/javascript" src="../js/proveedor.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>
--%>

     <script type="text/javascript" language="javascript" src="../js/jquery.validate.js"></script>

    <script type="text/javascript" language="javascript" src="../js/proveedor.js"></script>

    <script type="text/javascript" language="javascript" src="../js/JSON.js"></script>

    <script type="text/javascript" src="../js/jquery.maskedinput-1.2.2.min.js"></script>

    <script type="text/javascript" src="../js/jquery.alphanumeric.pack.js"></script>

    
    <script type="text/javascript">
	document.getElementById('tab1').setAttribute('class', 'tab active');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    
    
    //Documen ready
    $(document).ready(function() {
    deshabilitarCampos();
    
    //Se muestran solo los botones necesarios
    $("input[id*=imgbtNuevo]").css("display", "inline");
    $("input[id*=imgbtGuardar]").css("display", "none");
    $("input[id*=imgbtdMod]").css("display", "none");
    $("input[id*=imgbtBorrar]").css("display", "none");
    $("input[id*=imgbtEditar]").css("display", "none");
    $("input[id*=imgbtCancel]").css("display", "inline");
    
    
    $("input[id*=tbTelefono]").numeric({allow:"()-"});
    $("input[id*=tbNumDoc]").numeric({allow:"-"});
    $("input[id*=tbFechaNac]").mask("99/99/9999");
        
    $.validator.addMethod("fecha", 
              function(value, element) {
                 return validarFecha(value);
               }
     )
     
            $("#aspnetForm").validate({
                //definir reglas
                rules: {  
                    <%= tbNumDoc.UniqueID %>: { required: true},
                    <%= tbApellido.UniqueID %>: { required: true},
                    <%= tbDireccion.UniqueID %>: { required: true}
               
                },
                //mensajes a mostrar en caso que no se cumplan las reglas
                 messages: {
                   <%= tbNumDoc.UniqueID %>: { required: "Campo Obligatorio"},
                   <%= tbApellido.UniqueID %>: { required: "Campo Obligatorio"},
                   <%= tbDireccion.UniqueID %>: { required: "Campo Obligatorio"}
                  },
                
                //llamar a funcion guardar en caso que se hallan validados
                //los campos requeridos
	            submitHandler: function() {
	                guardarMod();
	            } 
            }); //validador fin
    });
    
/* Funcion que valida la fecha ingresado por un cliente */
function validarFecha(fecha)
{ 
    var dia  =  parseInt(fecha.substring(0,2),10);
    var mes  =  parseInt(fecha.substring(3,5),10);
    var anho =  parseInt(fecha.substring(6),10);
            
    anhoMaximo  = 1990;
    anhoMinimo  = 1949;
              
    //Controlo que el anho ingresado no sea superior o inferior a los anhos permitidos 
    if (anho > anhoMaximo || anho < anhoMinimo) return false;
    //Controlo que el mes ingresado no supere los 12 meses
    if (mes > 12 || dia==0 || mes == 0) return false;
    //Control de meses
    if(mes == 4 || mes == 6 || mes == 9 || mes == 11){
    //Si los dia de estos meses superan 30 retorna false
    if(dia > 30){
        return false;
                }//Si el mes ingresado es 2 = febrero
              }else if(mes == 2){
              //Controlo si el anho es bisisto
                if(bisiesto(anho)){
                //Si lo es controlo que no supere los 29 dias
                    if(dia > 29){
                        return false;
                    }//En caso contrario que no supere los 28 dias
                }else if(dia > 28){
                    return false;
                }
                 //Si no es ninguno de los mese anteriores que no sea mayor a 30
                }else if(dia > 31){
                    return false;
                 }
                  return true;
        }
        
/*Devuelve true si es bisiesto el anho ingresado*/
function bisiesto(anho){
    if ( ( anho % 100 != 0) && ((anho % 4 == 0) || (anho % 400 == 0))) 
        return true;
          else 
              return false;
}

function cancela() {
// Llama a la función cancelar definica en el .js
    cancelar();
    // Oculta la validacion
    $("#aspnetForm").valid();
            
    return false;
} 
    </script>

    <div style="display: block;" id="content_1" class="content">
        <%--<form id="form2" runat="server">--%>
            <table style="width: 100%">
                <tr>
                    <td>
                        <label id="lbMsn">
                        </label>
                        <%--<asp:Label ID="lbMsn" runat="server" ForeColor="red"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset id="basic">
                            <center>
                                <table runat="server" style="width: 100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lbRazonSocial" Text="Razon Social" runat="server" Font-Bold="True"
                                                ForeColor="Gray"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbRazonSocial" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbNombre" Text="Nombre" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbNombre" runat="server" Width="210px" MaxLength="30"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbApellido" Text="Apellido" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbApellido" runat="server" Width="210px" MaxLength="30"></asp:TextBox><font
                                                color="red">*</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lbTipoDoc" Text="Tipo de Documento" runat="server" Font-Bold="True"
                                                ForeColor="Gray"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="chTipoDoc" runat="server" DataSourceID="dsTipoDoc" DataTextField="descripcion"
                                                DataValueField="id_tipo_doc">
                                            </asp:DropDownList><asp:SqlDataSource ID="dsTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:Personal %>"
                                                SelectCommand="SELECT [id_tipo_doc], [descripcion] FROM [TIPO_DOC] where [borrado] = 'N'">
                                            </asp:SqlDataSource>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbNumDoc" Text="Numero de Documento" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbNumDoc" runat="server" Width="70px" MaxLength="10"></asp:TextBox><font
                                                color="red">*</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbDireccion" Text="Direccion" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbDireccion" TextMode="MultiLine" runat="server" Width="350px"></asp:TextBox><font
                                                color="red">*</font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbTelefono" Text="Telefono" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="tbTelefono" runat="server" Width="105px" MaxLength="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <%--<tr>
                                        <td align="right" class="labels">
                                            <asp:Label ID="lbSexo" Text="Sexo" runat="server" Font-Bold="True"></asp:Label>&nbsp;
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="chSexo" runat="server" Font-Bold="True">
                                                <asp:ListItem>Femenino</asp:ListItem>
                                                <asp:ListItem>Masculino</asp:ListItem>
                                            </asp:DropDownList>&nbsp;
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td colspan="2">
                                        </td>
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
                                        <asp:ImageButton ID="imgbtNuevo" ToolTip="Nuevo" runat="server" OnClientClick="nuevo(); return false;"
                                            ImageUrl="../images/new.ico" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtGuardar" ToolTip="Guardar" runat="server" ImageUrl="../images/save.png" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtdMod" ToolTip="Modificar" runat="server" ImageUrl="../images/save.png" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtEditar" ToolTip="Editar" runat="server" OnClientClick="editar(); return false;"
                                            ImageUrl="../images/edit.png" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtBorrar" ToolTip="Eliminar" runat="server" ImageUrl="../images/delete.ico"
                                            OnClientClick="popupEliminarPersona();" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtCancel" ToolTip="Cancelar" runat="server" OnClientClick="cancela(); return false;"
                                            ImageUrl="../images/cancel.png" />
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <!-- Tabla Dinamica -->
                        <div id="divContenedor">
                            <!-- tabla donde se genera la tabla dinamica-->
                            <table cellpadding="0" cellspacing="0" border="1" class="display" id="tablaPersonas">
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
                <div id="eliminarPersona" title="Eliminar Proveedor">
                    <!-- Mensaje de Confirmacion -->
                    Esta seguro que desea eliminar al proveedor seleccionado?
                </div>
            </div>
       <%-- </form>--%>
    </div>
</asp:Content>
