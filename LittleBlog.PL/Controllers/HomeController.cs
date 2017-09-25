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
        public ActionResult Index()
        {
            return RedirectToAction("PreviewArticles", "Articles", new { startWith=0, count=10 });
        }

        [Route("about")]
        public ActionResult About()
        {
            return View("About");
        }
    }
}