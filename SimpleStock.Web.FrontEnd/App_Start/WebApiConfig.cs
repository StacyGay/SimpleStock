using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SimpleStock.Web.FrontEnd
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{company}/{store}/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "AccountApi",
				routeTemplate: "api/Account/{id}",
				defaults: new { controller = "Account", id = RouteParameter.Optional }
			);
		}
	}
}
