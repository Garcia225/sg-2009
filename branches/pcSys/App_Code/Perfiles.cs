using System;
using System.Collections.Specialized;
using System.Configuration;
using GotDotNet.ApplicationBlocks.Data;
using log4net;

namespace SeguridadEnAspNet
{
	/// <summary>
	/// Clase para manejo de Perfiles.
	/// </summary>
	public class Perfiles
	{
		private static SqlServer _dataWorker = new SqlServer();
		private string connSql = ConfigurationSettings.AppSettings["SqlServerConnectionString"];
		private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		

		/// <summary>
		/// 
		/// </summary>
		public Perfiles()
		{}

		/// <summary>
		/// Devuelve un DataSet con los Paginas de un Perfil por Id
		/// </summary>
		/// <param name="idPerfil"></param>
		/// <returns></returns>
		public static System.Data.DataSet GetPaginas(int idPerfil)
		{
			System.Data.DataSet ds = new System.Data.DataSet();
			try
			{
                ds = _dataWorker.ExecuteDataset(ConfigurationSettings.AppSettings["SqlServerConnectionString"], "sp_login_perfilespagina_x_perfiles", new object[] {'1',idPerfil,"" });
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return ds;
		}

		/// <summary>
		/// Devuelve un DataSet con los Paginas de un Perfil por el Nombre del Perfil
		/// </summary>
		/// <param name="Perfil"></param>
		/// <returns></returns>
		public static System.Data.DataSet GetPaginas(string Perfil)
		{
			System.Data.DataSet ds = new System.Data.DataSet();
			try
			{
                ds = _dataWorker.ExecuteDataset(ConfigurationSettings.AppSettings["SqlServerConnectionString"], "sp_login_perfilespagina_x_perfiles", new object[] { '2',0,Perfil });
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return ds;
		}


		/// <summary>
		/// Determina si una pagina está habilitada
		/// </summary>
		/// <param name="page"></param>
		/// <param name="Perfil"></param>
		/// <returns></returns>
		public static bool IsPageEnabled( string pageName,string Perfil)
		{
			CheckCache(Perfil);
   

			bool result=false;
			try
			{
				System.Data.DataSet ds = (System.Data.DataSet)System.Web.HttpContext.Current.Cache.Get(Perfil);
				//System.Data.DataView dv = new System.Data.DataView(ds.Tables[0]);
                System.Data.DataTable dt = ds.Tables[0];
               



                foreach (System.Data.DataRow row in dt.Rows)
                {

                    if (pageName.StartsWith (row[2].ToString()))
                    {
                        result = true;
                    }
                }
                
                /*

                dv.RowFilter = "url='" + pageName + "'";
                
				if( dv.Count > 0 )
					result=true;*/
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return result;
		}

		/// <summary>
		/// Determina si una pagina está habilitada
		/// </summary>
		/// <param name="page"></param>
		/// <returns></returns>
		public static bool IsActionEnabled( string actionName,string Perfil )
		{
			//Por las dudas si se invalido el Cache despues de un login
			CheckCache(Perfil);

			bool result=false;
			try
			{
				System.Data.DataSet ds = (System.Data.DataSet)System.Web.HttpContext.Current.Cache.Get(Perfil);
				System.Data.DataView dv = new System.Data.DataView(ds.Tables[0]);
				dv.RowFilter = "url='" + actionName + "' and EsAccion=1";
				if( dv.Count > 0 )
					result=true;
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return result;
		}

		/// <summary>
		/// Chequea el Cache
		/// </summary>
		/// <param name="Perfil"></param>
		private static void CheckCache( string Perfil)
		{
			try
			{
				if( System.Web.HttpContext.Current.Cache.Get(Perfil) == null )
					SeguridadEnAspNet.UserCache.AddPaginasToCache(Perfil, SeguridadEnAspNet.Perfiles.GetPaginas(Perfil), System.Web.HttpContext.Current );
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}
		}


	} //class
} //namespace
