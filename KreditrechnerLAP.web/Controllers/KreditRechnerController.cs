using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KreditrechnerLAP.web.Controllers
{
    public class KreditRechnerController : Controller
    {
        [HttpGet]
        public ActionResult KreditRechner()
        {
            return View();
        }
    }
}