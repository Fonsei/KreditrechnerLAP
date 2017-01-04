using KreditrechnerLAP.logic;
using onlineKredit.web.Models;
using RazorPDF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace KreditrechnerLAP.web.Controllers
{
    public class BestaetigenController : Controller
    {
        [HttpGet]
        public ActionResult Index(bool erfolgreich)
        {
            Debug.WriteLine("");
            Debug.Indent();

            Debug.Unindent();
            return View(erfolgreich);
        }
    }
}