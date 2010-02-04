using System;
using System.Web.Security;
using System.Security.Principal;

namespace SeguridadEnAspNet
{
	/// <summary>
	/// Clase que administra la seguridad y se 
	/// adjunta al User del HTTPContext.
	/// </summary>
	public class FormsPrincipal :IPrincipal,IMyAppPrincipal
	{

		private IIdentity _identity;
		private string [] _roles;
		private string _Perfil;

		
		public FormsPrincipal( IIdentity identity, string [] roles)
		{
			_identity = identity;
			_roles = roles;
		}
		
		public FormsPrincipal( IIdentity identity, string [] roles, string Perfil)
		{
			_identity = identity;
			_roles = roles;
			_Perfil = Perfil;
		}

		public FormsPrincipal( IIdentity identity, string Perfil)
		{
			_identity = identity;
			_Perfil = Perfil;
		}

		

		//Propiedad que utilizaremos para saber si el usuario tiene o no habilitado
		//el acceso a una determinada pagina
		public bool IsPageEnabled(string pageName)
		{
			return Perfiles.IsPageEnabled( pageName, this._Perfil );  
		}

		/// <summary>
		/// Propiedad con el Perfil del Usuario
		/// </summary>
		public string Perfil
		{
			get
			{
				return _Perfil;
			}
			set
			{
				_Perfil = value;
			}
		}
		



		#region IPrincipal Members

		public IIdentity Identity
		{
			get
			{
				return _identity;
			}
		}

		public bool IsInRole(string role)
		{
			// TODO:  Add FormsPrincipal.IsInRole implementation
			return false;
		}


		#endregion


			} //class
} //namespace
