using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleStock.Web.FrontEnd.Controllers
{
    public class HomeController : Controller
    {
	    public HomeController()
	    {
		    Debug.WriteLine("new constructor");
	    }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

	    public ActionResult Account()
	    {
		    return View();
	    }
    }
}