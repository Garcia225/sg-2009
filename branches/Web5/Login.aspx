<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SeguridadEnAspNet.WebTest.Login.Login" %>

<?xml version="1.0" ?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
    <link rel="stylesheet" type="text/css" href="./_css/admin.css" media="all" />

</head>

<body>
    <div id="wrapper">
    </div>
    <div id="ctr" align="center">
        <div class="login">
            <div class="login-form">
                <img src="./_images/login.png" alt="Login" />
                <form id="loginForm" runat="server">
                    <asp:Label ID="Msg" ForeColor="maroon" runat="server" />
                    <div class="form-block">
                        <div class="inputlabel">Username</div>
                        <div><asp:TextBox ID="txtUser" runat="server"  /></div>
                        <div class="inputlabel">Password</div>
                        <div><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></div>
                        <div align="left"><asp:Button ID="btnSubmit" Text="Login" OnClick="btnSubmit_Click" 
                                runat="server" /></div>
                    </div>
                </form>
            </div>
            <div class="login-text">
                <div class="ctr"><img src="./_images/security.png" width="64" height="64" alt="security" /></div>
                <p>Bienvenido a pCsys</p>
                <p>Utilice un usuario y una contraseña valida para poder acceder a la intefaz de administraci&oacute;n</p>
            </div>
            <div class="clr"></div>
        </div>
    </div>
    <div id="break">
    </div>
    <noscript>Atención! Javascript debe estar habilitado para proporcionar operaciones al Administrador</noscript>
    <div class="footer" align="center">
        <div align="center"><a href="#">pCsys</a> es un software diseñado para Sistema de Gestión 2009</div>
    </div>
</body>
</html>
