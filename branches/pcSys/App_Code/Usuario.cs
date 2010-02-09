using System;
using System.Configuration;
using GotDotNet.ApplicationBlocks.Data;
using log4net;



namespace SeguridadEnAspNet
{
	/// <summary>
	/// Clase para manejo de Usuarios.
	/// </summary>
	public class Usuario
	{
		private static SqlServer _dataWorker = new SqlServer();
		private string connSql = ConfigurationSettings.AppSettings["SqlServerConnectionString"];
		private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public Usuario()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Devuelve el Perfil de un usuario
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="clave"></param>
		/// <returns></returns>
		public int GetIdPerfil(string usuario, string clave)
		{
			int idPerfil=0;
			try
			{
                System.Data.DataSet ds = _dataWorker.ExecuteDataset(connSql, "sp_login_usuarios_x_usuario", new object[] { '3', 1000, usuario, clave });
				if( ds.Tables[0].Rows.Count > 0 )
					idPerfil = Convert.ToInt32(ds.Tables[0].Rows[0]["idPerfil"]);

			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return idPerfil;
		}

		/// <summary>
		/// Devuelve el Perfil de un usuario
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="clave"></param>
		/// <returns></returns>
		public int GetIdPerfil(string usuario)
		{
			int idPerfil=0;
			try
			{
				System.Data.DataSet ds = _dataWorker.ExecuteDataset( connSql, "sp_login_usuarios_x_usuario", new object[] {'2', 1000, usuario});
				if( ds.Tables[0].Rows.Count > 0 )
					idPerfil = Convert.ToInt32(ds.Tables[0].Rows[0]["idPerfil"]);

			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return idPerfil;
		}


		/// <summary>
		/// Devuelve el Perfil de un usuario
		/// </summary>
		/// <param name="usuario"></param>
		/// <param name="clave"></param>
		/// <returns></returns>
		public string GetPerfil(string usuario, string clave)
		{
			string Perfil="";
			try
			{
                System.Data.DataSet ds = _dataWorker.ExecuteDataset(connSql, "sp_login_usuarios_x_usuario", new object[] {'3',1000, usuario, clave });				
				if( ds.Tables[0].Rows.Count > 0 )
                    Perfil = Convert.ToString(ds.Tables[0].Rows[0]["descripcion"]);

			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return Perfil;
		}

		/// <summary>
		/// Devuelve el Perfil de un usuario
		/// </summary>
		/// <param name="usuario"></param>
		/// <returns></returns>
		public string GetPerfil(string usuario)
		{
			string Perfil="";
			try
			{
                System.Data.DataSet ds = _dataWorker.ExecuteDataset(connSql, "sp_login_usuarios_x_usuario", new object[] {'2', 1000, usuario });
				if( ds.Tables[0].Rows.Count > 0 )
                    Perfil = Convert.ToString(ds.Tables[0].Rows[0]["descripcion"]);

			}
			catch( Exception ex )
			{
				log.Error( ex );
			}

			return Perfil;
		}

	} //class
} //namespace
