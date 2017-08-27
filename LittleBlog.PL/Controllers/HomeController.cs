using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LittleBlog.PL.Controllers
{
    [RoutePrefix("home")]
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return RedirectToAction("PreviewArticles", "Articles");
        }

        [Route("about")]
        public ActionResult About()
        {
            return View("About");
        }
    }
}