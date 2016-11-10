using KreditrechnerLAP.web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("GET - KreditRechnerController - KreditRechner");
            Debug.Indent();


            Debug.Unindent();
            return View();
        }

        [HttpPost]
        public ActionResult KreditRechner(KreditRechnerModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - KreditRechner");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic

                return RedirectToAction("Finanzielles");
            }
            Debugger.Break();
            Debug.Unindent();
            return View(model);
        }

        public ActionResult Finanzielles()
        {
            Debug.WriteLine("GET - KreditRechnerController - Finanzielles");
            Debug.Indent();


            Debug.Unindent();
            return View();
        }

        [HttpPost]
        public ActionResult Finanzielles(KreditRechnerModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Finanzielles");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic

                return RedirectToAction("PersoenlicheDaten");
            }

            Debug.Unindent();
            return View(model);
        }


        public ActionResult PersoenlicheDaten()
        {
            Debug.WriteLine("GET - KreditRechnerController - PersoenlicheDaten");
            Debug.Indent();


            Debug.Unindent();
            return View();
        }

        [HttpPost]
        public ActionResult PersoenlicheDaten(PersoenlicheDatenModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Finanzielles");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic


                return RedirectToAction("Finanzielles");
            }

            Debug.Unindent();
            return View(model);
        }
    }
}