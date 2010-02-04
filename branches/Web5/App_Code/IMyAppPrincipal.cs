using System;

namespace SeguridadEnAspNet
{
	/// <summary>
	/// Interface para implementar en las Clases que manejen IPrincipal.
	/// </summary>
	public interface IMyAppPrincipal
	{
		bool IsPageEnabled(string pageName);
//		bool IsActionEnabled(string actionName);
		string Perfil
		{
			get;
			set;
		}
	}
}
