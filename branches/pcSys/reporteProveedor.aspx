<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="reporteProveedor.aspx.cs" Inherits="Reportes_reporteProveedor" Title="Reporte Proveedor" %>
<%@ register assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
        namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" EnableViewState="false" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <script type="text/javascript">
	document.getElementById('tab1').setAttribute('class', 'tab');
    document.getElementById('tab2').setAttribute('class', 'tab');
    document.getElementById('tab3').setAttribute('class', 'tab');
    document.getElementById('tab4').setAttribute('class', 'tab');
    document.getElementById('tab5').setAttribute('class', 'tab active');
    </script>
    <div style="display: block;" id="content_5" class="content">
    <center>
    <table width="100%">
        <tr>
            <td>
                <rsweb:ReportViewer ID="rvProveedor" runat="server" Font-Names="Verdana" Font-Size="8pt"
                    Style="width: 100%; text-align: center; margin-left: auto; margin-right: auto;"
                    ZoomMode="PageWidth" Height="400px" Width="400px">
                    <LocalReport ReportPath="Reportes\Proveedor.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsProveedor_spProveedores" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
                    TypeName="dsProveedorTableAdapters.spProveedoresTableAdapter"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    </center>
    </div>
</asp:Content>
