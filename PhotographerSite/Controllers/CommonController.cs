using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotographerSite.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        public ActionResult CreateMenu()
        {
            GenericMenu();
            return View();
        }

        private string  GenericMenu()
        {
            throw new NotImplementedException();
        }

        public ActionResult GetLogoMaster(string sysname)
        {
            throw new NotImplementedException();
        }
    }
}
