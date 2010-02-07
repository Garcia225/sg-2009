using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using SeguridadEnAspNet;


namespace SeguridadEnAspNet.WebTest.Login
{
    /// <summary>
    /// Login page
    /// </summary>
    /// 

    public partial class Login : System.Web.UI.Page
    {



        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void btnSubmit_Click(object sender, System.EventArgs e)
        {

            string user = txtUser.Text;
            string password = txtPassword.Text;

            //Chequeo de usuario y contraseña
            SeguridadEnAspNet.Usuario oUser = new SeguridadEnAspNet.Usuario();
            string perfil = oUser.GetPerfil(user, password);
            if (perfil.Length > 0) // perfil vacio significa que no fue encontrado
            {
                //Invoca a componente que se encarga del Cache de los datos
                //en este caso de las páginas a las que el perfil tiene acceso
                SeguridadEnAspNet.UserCache.AddPaginasToCache(perfil, SeguridadEnAspNet.Perfiles.GetPaginas(perfil), System.Web.HttpContext.Current);

                // Crea un ticket de Autenticacion de forma manual, 
                // donde guardaremos información que nos interesa
                FormsAuthenticationTicket authTicket =
                        new FormsAuthenticationTicket(2,  // version
                        user,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(60),
                        false,
                        perfil, // guardo el perfil del usuario
                        FormsAuthentication.FormsCookiePath);
                // Encripto el Ticket.
                string crypTicket = FormsAuthentication.Encrypt(authTicket);

                // Creo la Cookie
                HttpCookie authCookie =
                        new HttpCookie(FormsAuthentication.FormsCookieName,
                        crypTicket);

                Response.Cookies.Add(authCookie);

                // Redirecciono al Usuario - Importante!! no usar el RedirectFromLoginPage
                // Para que se puedan usar las Cookies de los HttpModules
                Response.Redirect(FormsAuthentication.GetRedirectUrl(user, false));
            }
        }
    }
}