﻿using KreditrechnerLAP.logic;
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
        /*######################KREDIT RECHNER###############################*/
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
                Kunde neuerKunde = KreditInstitut.ErzeugeKunde();

                if (neuerKunde != null && KreditInstitut.KreditRahmenSpeichern(model.Kreditbetrag, model.Laufzeit, neuerKunde.ID))
                {
                    /// ich benötige für alle weiteren Schritte die ID 
                    /// des angelegten Kunden. Damit ich diese bei der nächsten Action 
                    /// habe, speichere ich sie für diesen Zweck in ein Cookie 
                    Response.Cookies.Add(new HttpCookie("idKunde", neuerKunde.ID.ToString()));
                    /// gehe zum nächsten Schritt 
                    return RedirectToAction("Finanzielles");
                }
            }
                Debugger.Break();
                Debug.Unindent();
                return View(model); 
        }

        /*#######################Finanzielles##################################*/
        [HttpGet]
        public ActionResult Finanzielles()
        {
            Debug.WriteLine("GET - KreditRechnerController - Finanzielles");
            Debug.Indent();
            FinanziellesModel model = new FinanziellesModel()
            {
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };

            Debug.Unindent();
            return View(model);
        }

        [HttpPost]
        public ActionResult Finanzielles(FinanziellesModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Finanzielles");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                /// speichere Daten über BusinessLogic
                if (KreditInstitut.FinanziellesSpeichern(
                                                model.NettoEinkommen,
                                                model.RatenVerpflichtung,
                                                model.Wohnkosten,
                                                model.EinkünfteAlimente,
                                                model.Unterhalt,
                                                model.ID_Kunde))
                {
                    return RedirectToAction("PersoenlicheDaten");
                }
            }
            Debugger.Break();
            Debug.Unindent();
            return View(model);
        }

        /*#######################PersoenlicheDaten#############################*/


        [HttpGet]
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
            
            PersoenlicheDatenModel model = new PersoenlicheDatenModel()
            {
                AlleBildungAngaben = alleBildungsAngaben,
                AlleFamilienStandAngaben = alleFamilienStandAngaben,
                AlleIdentifikationsAngaben = alleIdentifikationsAngaben,
                AlleStaatsbuergerschaftsAngaben = alleStaatsbuergerschaftsAngaben,
                AlleTitelAngaben = alleTitelAngaben,
                AlleWohnartAngaben = alleWohnartAngaben,
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
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
                if (KreditInstitut.PersoenlicheDatenSpeichern(
                                               model.ID_Titel,
                                               model.Geschlecht == Geschlecht.Männlich ? "m" : "w",
                                               model.GeburtsDatum,
                                               model.Vorname,
                                               model.Nachname,
                                               model.ID_TitelNachstehend,
                                               model.ID_Bildung,
                                               model.ID_Familienstand,
                                               model.ID_Identifikationsart,
                                               model.IdentifikationsNummer,
                                               model.ID_Staatsbuergerschaft,
                                               model.ID_Wohnart,
                                               model.ID_Kunde))
                {
                    return RedirectToAction("Arbeitgeber");
                }
            }

            Debug.Unindent();
            return View(model);
        }


        /*######################Arbeitgeber####################################*/

        [HttpGet]
        public ActionResult Arbeitgeber()
        {
            Debug.WriteLine("Get - KreditRechnerController - Arbeitgeber");
            Debug.Indent();
            List<BeschaeftigungsArtModel> alleBeschaeftigungen = new List<BeschaeftigungsArtModel>();
            List<BrancheModel> alleBranchen = new List<BrancheModel>();

            foreach (var beschaeftigung in KreditInstitut.BeschaeftigungsartenLaden())
            {
                alleBeschaeftigungen.Add(new BeschaeftigungsArtModel()
                {
                    ID = beschaeftigung.ID.ToString(),
                    Bezeichnung = beschaeftigung.Bezeichnung
                });
            }

            foreach (var branche in KreditInstitut.BranchenAngabenLaden())
            {
                alleBranchen.Add(new BrancheModel()
                {
                    ID = branche.ID.ToString(),
                    Bezeichnung = branche.Bezeichnung
                });
            }

            ArbeitgeberModel model = new ArbeitgeberModel()
            {
                AlleBeschaeftigungsarten = alleBeschaeftigungen,
                AlleBranchen = alleBranchen ,
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };

            Debug.Unindent();
            return View(model);
        }


        [HttpPost]
        public ActionResult Arbeitgeber(ArbeitgeberModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Arbeitgeber");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic
                if(KreditInstitut.ArbeitgeberDatenSpeichern(
                    model.Arbeitgeber,
                    model.ID_Beschaeftigungsart,
                    model.ID_Branche,
                    model.Beschaeftigtseit,
                    model.ID_Kunde))
                {
                    return RedirectToAction("Kontaktdaten");
                }
            }

            Debug.Unindent();
            return View(model);
        }

        /*######################Kontaktdaten####################################*/

        [HttpGet]
        public ActionResult Kontaktdaten()
        {
            Debug.WriteLine("Get - KreditRechnerController - Kontaktdaten");
            Debug.Indent();
            List<OrteModel> allorte = new List<OrteModel>();

            foreach (var ort in KreditInstitut.LadeAlleOrte())
                {
                    allorte.Add(new OrteModel(){
                    ID = ort.ID,
                    PLZ = ort.PLZ.ToString(),
                    Bezeichnung = ort.Bezeichnung.ToString()
                    });
                }

            KontaktdatenModel model = new KontaktdatenModel()
            {
                AlleOrte = allorte,
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };

            
            Debug.Unindent();
            return View(model);
        }


        [HttpPost]
        public ActionResult Kontaktdaten(KontaktdatenModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Kontaktdaten");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic
                if (KreditInstitut.KontaktDatenSpeichern(
                    model.Strasse,
                    model.Hausnummer,
                    model.ID_Ort,
                    model.Email,
                    model.Telefonnummer,
                    model.ID_Kunde))
                {
                    return RedirectToAction("KontoInformationen");
                }
            }

            Debug.Unindent();
            return View(model);
        }

        /*#################################################################*/
        /*######################KontoInformationen####################################*/

        [HttpGet]
        public ActionResult KontoInformationen()
        {
            Debug.WriteLine("Get - KreditRechnerController - KontoInformationen");
            Debug.Indent();

            KontoInformationenModel model = new KontoInformationenModel()
            {
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };
            Debug.Unindent();
            return View(model);
        }


        [HttpPost]
        public ActionResult KontoInformationen(KontoInformationenModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - KontoInformationen");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //Business Logic
                if (KreditInstitut.KontoInformationenSpeichern(
                    model.IBAN,
                    model.BIC,
                    model.Bankname,
                    model.ID_Kunde))
                {
                    return RedirectToAction("Zusammenfassung");
                }
            }

            Debug.Unindent();
            return View(model);
        }

        /*#################################################################*/

        /*######################Zusammenfassung####################################*/

        [HttpGet]
        public ActionResult Zusammenfassung()
        {
            Debug.WriteLine("GET - KonsumKredit - Zusammenfassung");

            /// ermittle für diese Kunden_ID
            /// alle gespeicherten Daten (ACHTUNG! das sind viele ....)
            /// gib Sie alle in das ZusammenfassungsModel (bzw. die UNTER-Modelle) 
            /// hinein.
            ZusammenfassungModel model = new ZusammenfassungModel();
            model.ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value);

            /// lädt ALLE Daten zu diesem Kunden (also auch die angehängten/referenzierten
            /// Entities) aus der DB
            Kunde aktKunde = KreditInstitut.KundeLaden(model.ID_Kunde);

            model.GewünschterBetrag = (int)aktKunde.Kredit.Betrag.Value;
            model.Laufzeit = aktKunde.Kredit.Zeitraum.Value;

            model.NettoEinkommen = (double)aktKunde.FinanzielleSituation.NettoEinkommen.Value;
            model.Wohnkosten = (double)aktKunde.FinanzielleSituation.Wohnkosten.Value;
            model.EinkünfteAlimenteUnterhalt = (double)aktKunde.FinanzielleSituation.EinkünfteAlimente.Value;
            model.UnterhaltsZahlungen = (double)aktKunde.FinanzielleSituation.Unterhaltszahlung.Value;
            model.RatenVerpflichtungen = (double)aktKunde.FinanzielleSituation.Ratenverpflichtung.Value;

            model.Geschlecht = aktKunde.Geschlecht == "m" ? "Herr" : "Frau";
            model.Vorname = aktKunde.Vorname;
            model.Nachname = aktKunde.Nachname;
            model.Titel = aktKunde.Titel?.Bezeichnung;
            model.GeburtsDatum = DateTime.Now;
            model.Staatsbuergerschaft = aktKunde.Staatsbuerger?.Bezeichnung;
            model.AnzahlUnterhaltspflichtigeKinder = -1;
            model.Familienstand = aktKunde.Familienstand?.Bezeichnung;
            model.Wohnart = aktKunde.Wohnart?.Bezeichnung;
            model.Bildung = aktKunde.Ausbildung?.Bezeichnung;
            model.Identifikationsart = aktKunde.IdentifikationsArt?.Bezeichnung;
            model.IdentifikationsNummer = aktKunde.Idendifikationsnummer;

            model.FirmenName = aktKunde.Arbeitgeber?.Firmenname;
            model.BeschäftigungsArt = aktKunde.Arbeitgeber?.Beschaeftigungsart?.Bezeichnung;
            model.Branche = aktKunde.Arbeitgeber?.Branche?.Bezeichnung;
            model.BeschäftigtSeit = aktKunde.Arbeitgeber?.BeschaeftigtSeit.Value.ToShortDateString();

            model.Strasse = aktKunde.Kontaktdaten?.Strasse;
            model.Hausnummer = aktKunde.Kontaktdaten?.Hausnummer;
            //model.Ort = aktKunde.Kontaktdaten?.Ort.PLZ;
            model.Mail = aktKunde.Kontaktdaten?.EMail;
            model.TelefonNummer = aktKunde.Kontaktdaten?.Telefonnummer;

            //model.NeuesKonto = (bool)aktKunde.Konto?.KontoNeu.Value;
            model.BankName = aktKunde.Konto?.Bankname;
            model.IBAN = aktKunde.Konto?.IBAN;
            model.BIC = aktKunde.Konto?.BIC;

            /// gib model an die View
            return View(model);
        }


        [HttpPost]
        public ActionResult Zusammenfassung(Konto model)
        {
            Debug.WriteLine("POST - KreditRechnerController - Zusammenfassung");
            Debug.Indent();


            Debug.Unindent();
            return View(model);
        }

        /*#################################################################*/

    }
}
