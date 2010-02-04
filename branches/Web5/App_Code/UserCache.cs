using System;
using log4net;


namespace SeguridadEnAspNet
{
	/// <summary>
	/// Administra el Cache de los Paginas por Perfiles
	/// </summary>
	public class UserCache
	{
		private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public UserCache()
		{}

		/// <summary>
		/// Agrega los Paginas de un Perfil al Cache
		/// </summary>
		/// <param name="idPerfil"></param>
		/// <param name="dsPaginas">Paginas</param>
		/// <param name="context">El HTTPContext que se esta ejecutando</param>
		public static void AddPaginasToCache( int idPerfil, System.Data.DataSet dsPaginas, System.Web.HttpContext context)
		{
			AddPaginas( idPerfil.ToString(), dsPaginas, context );
		}

		/// <summary>
		/// Agrega los Paginas de un Perfil al Cache
		/// </summary>
		/// <param name="Perfil"></param>
		/// <param name="dsPaginas">Paginas</param>
		/// <param name="context">El HTTPContext que se esta ejecutando</param>
		public static void AddPaginasToCache( string Perfil, System.Data.DataSet dsPaginas, System.Web.HttpContext context)
		{
			AddPaginas( Perfil, dsPaginas, context );
		}


		/// <summary>
		/// Agrega los Paginas de un Perfil al Cache
		/// </summary>
		/// <param name="idPerfil"></param>
		/// <param name="dsPaginas">Paginas</param>
		/// <param name="context">El HTTPContext que se esta ejecutando</param>
		private static void AddPaginas( string key, System.Data.DataSet dsPaginas, System.Web.HttpContext context)
		{
			try
			{
				if( context.Cache.Get( key ) != null )
					context.Cache.Remove( key); //remuevo si lo encuentra para reemplazarlo

				context.Cache.Add( key, 
					dsPaginas,null, 
					DateTime.Now.AddMonths(1) , 
					TimeSpan.Zero , 
					System.Web.Caching.CacheItemPriority.High, null); //agrego al cache
			}
			catch( Exception ex )
			{
				log.Error( ex );
			}
		}

		/// <summary>
		/// Limpia el Cache
		/// </summary>
		public static void InvalidarCache(System.Web.HttpContext context)
		{
			try
			{
				System.Collections.IDictionaryEnumerator num = context.Cache.GetEnumerator();
				while(num.MoveNext())
					context.Cache.Remove(num.Key.ToString()); 
			}
			catch( Exception ex )
			{
				log.Error(ex);
			}

		}

		public static bool IsInCache( string key, System.Web.HttpContext context)
		{
			bool result=false;
			try
			{
				if( context.Cache.Get( key ) != null )
					result=true;
			}
			catch( Exception ex )
			{
				log.Error(ex);
			}

			return result;
		}
	} //class
}//namespace
