<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CtaCteProveedor.aspx.cs" Inherits="pruebaMaster3" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
//Organiza los tabs
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab active');
</script>

<div style="display: block;" id="content_4" class="content">


<div id="divContenedor">
                <!-- tabla donde se genera la tabla dinamica-->
                <table cellpadding="0" cellspacing="0" border="1" class="display" id="tableFacturas">
                    <!-- Imagen loading, se oculta cuando se cargan todas las tablas -->
                    <tr>
                        <td align="center">
                            <%--<img id="imgLoad" src="../images/loader.gif" alt="" />--%>
                        </td>
                    </tr>
                </table>
            </div>
            
            
            
            
<table style="width: 100%; height: 171px;">
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td style="height: 21px">
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
        <tr>
            <td>
                esto es una prueba
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

