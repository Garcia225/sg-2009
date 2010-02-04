using System;
using System.Web;
using System.Security.Principal;
using System.Web.Security;

namespace SeguridadEnAspNet
{
	/// <summary>
	/// Modulo de Admistracion de la Seguridad
	/// Seguridad basada en Forms
	/// </summary>
	public class CustomAuthenticationModule : IHttpModule
	{
		/// <summary>
		/// 
		/// </summary>
		public CustomAuthenticationModule()
		{}

		/// <summary>
		/// Inicializa el HTTPModule y asigna los EventHandlers a cada Evento
		/// Esta es la parte donde se define a que eventos va a atender el HttpModule
		/// </summary>
		/// <param name="oHttpApp"></param>
		public void Init(HttpApplication oHttpApp)
		{
			// Se Registran los Manejadores de Evento que nos interesa
			oHttpApp.AuthorizeRequest += new EventHandler(this.AuthorizaRequest);
			oHttpApp.AuthenticateRequest += new EventHandler(this.AuthenticateRequest);
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{}

		/// <summary>
		/// Administra la autorización por Request
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AuthorizaRequest( object sender, EventArgs e)
		{	
			if (HttpContext.Current.User != null)
			{
				//Si el usuario esta Autenticado
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					if (HttpContext.Current.User is SeguridadEnAspNet.FormsPrincipal)
					{
						SeguridadEnAspNet.FormsPrincipal principal = (SeguridadEnAspNet.FormsPrincipal)HttpContext.Current.User;
						if( !principal.IsPageEnabled( HttpContext.Current.Request.Path) )
							HttpContext.Current.Server.Transfer("~/NoAutorizado.aspx"); 
					}
				}
			}

		}

		/// <summary>
		/// Autentica en Cada Request
		/// </summary>
		/// <param name="sender">HttpApplication</param>
		/// <param name="e"></param>
		private void AuthenticateRequest(object sender, EventArgs e)
		{
			if (HttpContext.Current.User != null)
			{
				//Si el usuario esta Autenticado
				if (HttpContext.Current.User.Identity.IsAuthenticated)
				{
					if (HttpContext.Current.User.Identity is FormsIdentity)
					{
						//Traigo el Rol que esta guardado en una Cookie encriptada
						FormsIdentity _identity = (FormsIdentity)HttpContext.Current.User.Identity;
						FormsAuthenticationTicket ticket = _identity.Ticket;
						string cookieName = System.Web.Security.FormsAuthentication.FormsCookieName;
						string userData =System.Web.HttpContext.Current.Request.Cookies[cookieName].Value;
						ticket  = FormsAuthentication.Decrypt(userData);

						string perfil="";
						if( userData.Length > 0 )
							perfil= ticket.UserData;

						//Se crea la clase y se asigna al CurrenUser del Contexto			
						HttpContext.Current.User = new SeguridadEnAspNet.FormsPrincipal(_identity, perfil);
					}
				}
			}
		}//AuthenticateRequest
		
	} //class

} //namespace
