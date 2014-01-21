using System.Web.Optimization;

namespace SimpleStock.Web.FrontEnd.App_Start
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			// example
			/*bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
				"~/Scripts/jquery.unobtrusive*",
				"~/Scripts/jquery.validate*"));*/

			bundles.Add(new StyleBundle("~/Content/app").Include(
				"~/Content/app.css"));

			bundles.Add(new ScriptBundle("~/app").Include(
				"~/Scripts/app.js"));
		}
	}
}