using KreditrechnerLAP.logic;
using KreditrechnerLAP.web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace KreditrechnerLAP.web.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        [HttpGet]
        public ActionResult Administration()
        {
            Debug.WriteLine("GET - AdministrationController - Administration");
            Debug.Indent();
            Debug.Unindent();
            return View();
        }

        [HttpPost]
        public ActionResult Administration(AnmeldeModel model)
        {
            Debug.WriteLine("POST - AdministrationController - Administration");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                //var md5 = new MD5CryptoServiceProvider();
                //var md5data = md5.ComputeHash(data);
                //string password = BitConverter.ToString(encodedBytes);

                if (KreditInstitut.Anmeldung(model.Nickname, model.Password))
                {
                    return RedirectToAction("Kreditantraege");
                }                
            }
            Debug.Unindent();
            return View(model);
        }

        [HttpGet]
        public ActionResult Kreditantraege()
        {
            Debug.WriteLine("GET - AdministrationController - Kreditantraege");
            Debug.Indent();
            List<KreditantragListeModel> Liste = new List<KreditantragListeModel>();
            using (var context = new dbKreditInstitutEntities())
            {
                foreach (var kunde in context.AlleKunden)
                {
                    if (kunde != null)
                    {
                        Kunde aktKunde = KreditInstitut.KundeLaden(kunde.ID);
                        KreditantragListeModel model = new KreditantragListeModel();
                        model.ID_Kunde = kunde.ID;
                        model.GewünschterBetrag = (int)aktKunde.Kredit.Betrag.Value;
                        model.Laufzeit = aktKunde.Kredit.Zeitraum.Value;

                        model.NettoEinkommen = (double)aktKunde.FinanzielleSituation.NettoEinkommen;
                        //model.Wohnkosten = (double)aktKunde.FinanzielleSituation.Wohnkosten;
                        //model.EinkünfteAlimenteUnterhalt = (double)aktKunde.FinanzielleSituation.EinkünfteAlimente;
                        //model.UnterhaltsZahlungen = (double)aktKunde.FinanzielleSituation.Unterhaltszahlung;
                        //model.RatenVerpflichtungen = (double)aktKunde.FinanzielleSituation.Ratenverpflichtung;

                        model.Geschlecht = aktKunde.Geschlecht == "m" ? "Herr" : "Frau";
                        //model.Vorname = aktKunde.Vorname;
                        model.Nachname = aktKunde.Nachname;
                        //model.Titel = aktKunde.Titel?.Bezeichnung;
                        //model.GeburtsDatum = aktKunde.Geburtsdatum;
                        model.Staatsbuergerschaft = aktKunde.Staatsbuerger?.Bezeichnung;
                        //model.AnzahlUnterhaltspflichtigeKinder = aktKunde.Kinder;
                        model.Familienstand = aktKunde.Familienstand?.Bezeichnung;
                        //model.Wohnart = aktKunde.Wohnart?.Bezeichnung;
                        //model.Bildung = aktKunde.Ausbildung?.Bezeichnung;
                        model.Identifikationsart = aktKunde.IdentifikationsArt?.Bezeichnung;
                        model.IdentifikationsNummer = aktKunde.Idendifikationsnummer;

                        //model.FirmenName = aktKunde.Arbeitgeber?.Firmenname;
                        //model.BeschäftigungsArt = aktKunde.Arbeitgeber?.Beschaeftigungsart?.Bezeichnung;
                        //model.Branche = aktKunde.Arbeitgeber?.Branche?.Bezeichnung;
                        //model.BeschäftigtSeit = aktKunde.Arbeitgeber?.BeschaeftigtSeit.Value.ToShortDateString();

                        //model.Strasse = aktKunde.Kontaktdaten?.Strasse;
                        //model.Hausnummer = aktKunde.Kontaktdaten?.Hausnummer;
                        ////model.Ort = aktKunde.Kontaktdaten.FKOrt.Value;
                        //model.PLZ = aktKunde.Kontaktdaten?.Ort.PLZ;
                        //model.Ort = aktKunde.Kontaktdaten?.Ort.Bezeichnung;
                        model.Mail = aktKunde.Kontaktdaten?.EMail;
                        //model.TelefonNummer = aktKunde.Kontaktdaten?.Telefonnummer;

                        ////model.NeuesKonto = (bool)aktKunde.Konto?.KontoNeu;
                        //model.BankName = aktKunde.Konto?.Bankname;
                        //model.IBAN = aktKunde.Konto?.IBAN;
                        //model.BIC = aktKunde.Konto?.BIC;

                        Liste.Add(model);
                    }
                }
            }
            
                

            Debug.Unindent();
            return View(Liste);

        }


        [HttpPost]
        public ActionResult Kreditantraege(KreditantragListeModel model)
        {
            Debug.WriteLine("POST - AdministrationController - Kreditantraege");
            Debug.Indent();
            if (ModelState.IsValid)
            {
               
            }
            Debug.Unindent();
            return View(model);
        }
    }
}