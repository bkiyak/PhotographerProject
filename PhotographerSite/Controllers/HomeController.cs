using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographerSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return RedirectToAction("ContentBlock", "Content", routeValues: new { url ="Anasayfa"});
        }
    }
}
