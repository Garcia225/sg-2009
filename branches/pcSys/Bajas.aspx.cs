using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace SeguridadEnAspNet.WebTest
{
	/// <summary>
	/// Summary description for Pagina1.
	/// </summary>
	public partial class Bajas : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
        {
            SeguridadEnAspNet.IMyAppPrincipal principal = (SeguridadEnAspNet.IMyAppPrincipal)HttpContext.Current.User;
            lblPerfil.Text = principal.Perfil;
            lblName.Text = HttpContext.Current.User.Identity.Name;
		}

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
	}
}
