using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using SimpleStock.Data.Models;

namespace SimpleStock.Web.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			/*var builder = new ODataConventionModelBuilder();
			builder.EntitySet<Store>("Store");
			builder.EntitySet<Company>("Companies");
			builder.EntitySet<ProductCategory>("ProductCategory");
			builder.EntitySet<Product>("Product");
			builder.EntitySet<User>("User");
			builder.EntitySet<Inventory>("Inventory");

			config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());*/
        }
    }
}
