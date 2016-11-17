using KreditrechnerLAP.logic;
using KreditrechnerLAP.web.Models;
using onlineKredit.web.Models;
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
        public ActionResult Finanzielles(FinanziellesModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Finanzielles");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic

                return RedirectToAction("PersoenlicheDaten");
            }
            Debugger.Break();
            Debug.Unindent();
            return View(model);
        }

        public ActionResult PersoenlicheDaten2()
        {
            return View();
        }

        public ActionResult PersoenlicheDaten()
        {
            


            Debug.WriteLine("GET - KreditRechnerController - PersoenlicheDaten");
            Debug.Indent();

            List<BildungsModel> alleBildungsAngaben = new List<BildungsModel>();
            List<FamilienStandModel> alleFamilienStandAngaben = new List<FamilienStandModel>();
            List<IdentifikationsModel> alleIdentifikationsAngaben = new List<IdentifikationsModel>();
            List<StaatsbuergerschaftsModel> alleStaatsbuergerschaftsAngaben = new List<StaatsbuergerschaftsModel>();
            List<TitelModel> alleTitelAngaben = new List<TitelModel>();
            List<WohnartModel> alleWohnartAngaben = new List<WohnartModel>();
            List<TitelNachstehendModel> alleTitelNachstehenAngaben = new List<TitelNachstehendModel>();

            /// Lade Daten aus Logic
            foreach (var bildungsAngabe in KreditInstitut.BildungsAngabenLaden())
            {
                alleBildungsAngaben.Add(new BildungsModel()
                {
                    ID = bildungsAngabe.ID.ToString(),
                    Bezeichnung = bildungsAngabe.Bezeichnung
                });
            }

            foreach (var familienStand in KreditInstitut.FamilienStandAngabenLaden())
            {
                alleFamilienStandAngaben.Add(new FamilienStandModel()
                {
                    ID = familienStand.ID.ToString(),
                    Bezeichnung = familienStand.Bezeichnung
                });
            }
            foreach (var identifikationsAngabe in KreditInstitut.IdentifikiationsAngabenLaden())
            {
                alleIdentifikationsAngaben.Add(new IdentifikationsModel()
                {
                    ID = identifikationsAngabe.ID.ToString(),
                    Bezeichnung = identifikationsAngabe.Bezeichnung
                });
            }
            foreach (var land in KreditInstitut.LaenderLaden())
            {
                alleStaatsbuergerschaftsAngaben.Add(new StaatsbuergerschaftsModel()
                {
                    ID = land.ID,
                    Bezeichnung = land.Bezeichnung
                });
            }
            foreach (var titel in KreditInstitut.TitelLaden())
            {
                alleTitelAngaben.Add(new TitelModel()
                {
                    ID = titel.ID.ToString(),
                    Bezeichnung = titel.Bezeichnung
                });
            }
            foreach (var wohnart in KreditInstitut.WohnartenLaden())
            {
                alleWohnartAngaben.Add(new WohnartModel()
                {
                    ID = wohnart.ID.ToString(),
                    Bezeichnung = wohnart.Bezeichnung
                });
            }
            


            PersönlicheDatenModel model = new PersönlicheDatenModel()
            {
                AlleBildungAngaben = alleBildungsAngaben,
                AlleFamilienStandAngaben = alleFamilienStandAngaben,
                AlleIdentifikationsAngaben = alleIdentifikationsAngaben,
                AlleStaatsbuergerschaftsAngaben = alleStaatsbuergerschaftsAngaben,
                AlleTitelAngaben = alleTitelAngaben,
                AlleWohnartAngaben = alleWohnartAngaben

                //ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };
            Debug.Unindent();
            return View(model);
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