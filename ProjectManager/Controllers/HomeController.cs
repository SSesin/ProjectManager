using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    /// <summary>
    /// Controller of the Home page
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Load the Home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
