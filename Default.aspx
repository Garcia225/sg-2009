<%@ Page language="c#" Inherits="SeguridadEnAspNet.WebTest._Default" CodeFile="Default.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="css/estilos.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			
			<br>
			<center>
			<table class="HeaderToolbar" cellSpacing="3" cellPadding="1" width="200" border="1">
				<tr  >
					<td>Perfil: 
						<asp:Label Runat=server ID="lblPerfil"></asp:Label>
					</td>
				</tr>
				<tr>
					<td>Usuario: 
						<asp:Label Runat=server ID="lblName"></asp:Label>
					</td>
				</tr>
			</table>
			<br>
			<table width=400 cellSpacing="5">
				<tr><td colspan=2 class=Chain>Podras ingresar segun tu perfil de usuario. Si no tienes acceso,<br>verás la página correspondiente.</td></tr>
				<tr>
					<td align=right><a href='altas.aspx'>Altas</a></td>
					<td><a href='bajas.aspx'>Bajas</a></td>
				</tr>
			</table>
			</center>
		</form>
	</body>
</HTML>
