using System.Web.Mvc;

namespace SimpleStock.Web.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}