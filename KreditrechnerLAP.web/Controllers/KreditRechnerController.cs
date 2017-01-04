using iTextSharp.text;
using iTextSharp.text.pdf;
using KreditrechnerLAP.freigabe;
using KreditrechnerLAP.logic;
using KreditrechnerLAP.web.Models;
using onlineKredit.web.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            KreditRechnerModel model = new KreditRechnerModel()
            {
                Kreditbetrag = 25000,  // default Werte
                Zeitraum = 12   // default Werte
            };
            int id = -1;
            if (Request.Cookies["idKunde"] != null && int.TryParse(Request.Cookies["idKunde"].Value, out id))
            {
                /// lade Daten aus Datenbank
                Kredit wunsch = KreditInstitut.KreditRahmenLaden(id);
                model.Kreditbetrag = (int)wunsch.Betrag.Value;
                model.Zeitraum = wunsch.Zeitraum.Value;
            }

            Debug.Unindent();
            return View(model);
        }

        [HttpPost]
        public ActionResult KreditRechner(KreditRechnerModel model)
        {
            Debug.WriteLine("POST - KreditRechnerController - KreditRechner");
            Debug.Indent();
            if (ModelState.IsValid)
            {
                Kunde neuerKunde = KreditInstitut.ErzeugeKunde();

                if (neuerKunde != null && KreditInstitut.KreditRahmenSpeichern(model.Kreditbetrag, model.Zeitraum, neuerKunde.ID))
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

            FinanzielleSituation finanziellesituation = KreditInstitut.FinanzielleSituationAngabenLaden(model.ID_Kunde);
            if (finanziellesituation != null)
            {
                model.NettoEinkommen = (double)finanziellesituation.NettoEinkommen;
                model.RatenVerpflichtung = (double)finanziellesituation.Ratenverpflichtung;
                model.Unterhalt = (double)finanziellesituation.Unterhaltszahlung; ;
                model.Wohnkosten = (double)finanziellesituation.Wohnkosten;
            }

                      

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

            Kunde kunde = KreditInstitut.PersoenlicheDatenLaden(model.ID_Kunde);
            if (kunde != null)
            {
                model.Geschlecht = !string.IsNullOrEmpty(kunde.Geschlecht) && kunde.Geschlecht == "m" ? KreditrechnerLAP.web.Models.Geschlecht.Männlich : KreditrechnerLAP.web.Models.Geschlecht.Weiblich;
                model.Vorname = kunde.Vorname;
                model.Nachname = kunde.Nachname;
                model.ID_Titel = kunde.FKTitel.HasValue ? kunde.FKTitel.Value : 0;
                model.GeburtsDatum = DateTime.Now;
                model.ID_Staatsbuergerschaft = kunde.FKStaatsbuergerschaft;
                model.ID_Familienstand = kunde.FKFamilienstand.HasValue ? kunde.FKFamilienstand.Value : 0;
                model.ID_Wohnart = kunde.FKWohnart.HasValue ? kunde.FKWohnart.Value : 0;
                model.ID_Bildung = kunde.FKAusbildung.HasValue ? kunde.FKAusbildung.Value : 0;
                model.ID_Identifikationsart = kunde.FKIdentifikationsArt.HasValue ? kunde.FKIdentifikationsArt.Value : 0;
                model.IdentifikationsNummer = kunde.Idendifikationsnummer;
                model.Zujung = false;
            }
            else
                kunde = new Kunde();

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
                int alter = KreditInstitut.Alter(model.GeburtsDatum);
                if (alter > 17)
                {
                   
                    if (KreditInstitut.PersoenlicheDatenSpeichern(
                                                   model.ID_Titel,
                                                   model.Geschlecht == Geschlecht.Männlich ? "m" : "w",
                                                   model.AnzahlUnterhaltspflichtigeKinder,
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
                        model.Zujung = false;
                        return RedirectToAction("Arbeitgeber");
                    }
                }
                else
                {
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

                    model.AlleBildungAngaben = alleBildungsAngaben;
                    model.AlleFamilienStandAngaben = alleFamilienStandAngaben;
                    model.AlleIdentifikationsAngaben = alleIdentifikationsAngaben;
                    model.AlleStaatsbuergerschaftsAngaben = alleStaatsbuergerschaftsAngaben;
                    model.AlleTitelAngaben = alleTitelAngaben;
                    model.AlleWohnartAngaben = alleWohnartAngaben;
                    model.Zujung = true;
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
                AlleBranchen = alleBranchen,
                Beschaeftigtseit = DateTime.Now,
                ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value)
            };

            Arbeitgeber arbeitgeberDaten = KreditInstitut.ArbeitgeberAngabenLaden(model.ID_Kunde);
            if (arbeitgeberDaten != null)
            {
                model.Beschaeftigtseit = arbeitgeberDaten.BeschaeftigtSeit.Value.Date;
                model.Arbeitgeber = arbeitgeberDaten.Firmenname;
                model.ID_Beschaeftigungsart = arbeitgeberDaten.FKBeschaeftigungsart.Value; ;
                model.ID_Branche = arbeitgeberDaten.FKBranche.Value;
            }

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

            Kontaktdaten daten = KreditInstitut.KontaktDatenLaden(model.ID_Kunde);
            if (daten != null)
            {
                model.Hausnummer = daten.Hausnummer;
                model.Strasse = daten.Strasse;
                model.Stiege = daten.Stiege;
                model.Tuer = daten.Tuer;
                model.ID_Ort = daten.FKOrt.Value;
                model.Email = daten.EMail;
                model.Telefonnummer = daten.Telefonnummer;
            }


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
                    model.Stiege,
                    model.Tuer,
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

            Konto daten = KreditInstitut.KontoInformationenLaden(model.ID_Kunde);
            if (daten != null)
            {
                model.Bankname = daten.Bankname;
                model.BIC = daten.BIC;
                model.IBAN = daten.IBAN;
                model.KontoNeu = Convert.ToBoolean( daten.KontoNeu);            }
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
                if (model.KontoNeu)
                {
                    model.Bankname = "Deutschebank AG";
                    model.IBAN = "Neuer IBAN";
                    model.BIC =  "Neuer BIC";
                }
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
            /// 

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
            model.GeburtsDatum = aktKunde.Geburtsdatum;
            model.Staatsbuergerschaft = aktKunde.Staatsbuerger?.Bezeichnung;
            model.AnzahlUnterhaltspflichtigeKinder = aktKunde.Kinder;
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
            //model.Ort = aktKunde.Kontaktdaten.FKOrt.Value;
            model.PLZ = aktKunde.Kontaktdaten?.Ort.PLZ;
            model.Ort = aktKunde.Kontaktdaten?.Ort.Bezeichnung;
            model.Mail = aktKunde.Kontaktdaten?.EMail;
            model.TelefonNummer = aktKunde.Kontaktdaten?.Telefonnummer;

            //model.NeuesKonto = (bool)aktKunde.Konto?.KontoNeu;
            model.BankName = aktKunde.Konto?.Bankname;
            model.IBAN = aktKunde.Konto?.IBAN;
            model.BIC = aktKunde.Konto?.BIC;
                
            /// gib model an die View
            return View(model);
        }

        [HttpGet]
        public ActionResult Zusammenfassungpdf()
        {
            Debug.WriteLine("POST - KreditRechnerController - Zusammenfassung");
            ZusammenfassungModel model = new ZusammenfassungModel();
            // model.ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value);
            model.ID_Kunde = 3;
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
            model.GeburtsDatum = aktKunde.Geburtsdatum;
            model.Staatsbuergerschaft = aktKunde.Staatsbuerger?.Bezeichnung;
            model.AnzahlUnterhaltspflichtigeKinder = aktKunde.Kinder;
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
            //model.Ort = aktKunde.Kontaktdaten.FKOrt.Value;
            model.PLZ = aktKunde.Kontaktdaten?.Ort.PLZ;
            model.Ort = aktKunde.Kontaktdaten?.Ort.Bezeichnung;
            model.Mail = aktKunde.Kontaktdaten?.EMail;
            model.TelefonNummer = aktKunde.Kontaktdaten?.Telefonnummer;

            //model.NeuesKonto = (bool)aktKunde.Konto?.KontoNeu;
            model.BankName = aktKunde.Konto?.Bankname;
            model.IBAN = aktKunde.Konto?.IBAN;
            model.BIC = aktKunde.Konto?.BIC;


            Debug.Unindent();
            return View(model);
        }

        /*#################################################################*/
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Beantragen(int id, bool? bestätigt)
        {
            if (bestätigt.HasValue && bestätigt.Value)
            {
                Debug.WriteLine("POST - KonsumKredit - Bestätigung");
                Debug.Indent();


                //int idKunde = int.Parse(Request.Cookies["idKunde"].Value);
                Kunde aktKunde = KreditInstitut.KundeLaden(id);
                //Response.Cookies.Remove("idKunde");

                bool istFreigegeben = Kreditfreigeben.FreigabeErteilt(
                                                          aktKunde.Geschlecht,
                                                            aktKunde.Vorname,
                                                            aktKunde.Nachname,
                                                            aktKunde.Familienstand.Bezeichnung,
                                                            (double)aktKunde.FinanzielleSituation.NettoEinkommen,
                                                            (double)aktKunde.FinanzielleSituation.Wohnkosten,
                                                            (double)aktKunde.FinanzielleSituation.EinkünfteAlimente,
                                                            (double)aktKunde.FinanzielleSituation.Unterhaltszahlung,
                                                            (double)aktKunde.FinanzielleSituation.Ratenverpflichtung);

                /// Rüfe Service/DLL auf und prüfe auf Kreditfreigabe
                Debug.WriteLine($"Kreditfreigabe {(istFreigegeben ? "" : "nicht")}erteilt!");

                Debug.Unindent();
                return RedirectToAction("Index", "Bestaetigen", new { erfolgreich = istFreigegeben, idKunde = id});

            }
            else
            {
                return RedirectToAction("Zusammenfassung");
            }
        }

        [HttpGet]
        public ActionResult Pdfexport()
        {
            Debug.WriteLine("");
            Debug.Indent();
            ZusammenfassungModel model = new ZusammenfassungModel();
            model.ID_Kunde = int.Parse(Request.Cookies["idKunde"].Value);
            Kunde aktKunde = KreditInstitut.KundeLaden(model.ID_Kunde);
            string kunde = aktKunde.Nachname;
            string Path = aktKunde.Nachname + ".pdf";
            string pdfaction = "Zusammenfassungpdf";


            Debug.Unindent();
            //new ActionAsPdf("Index", fullname) { FileName = "Test.pdf" };

            //return new ViewAsPdf(model)
            //{
            //    FileName = Path
            //};

            return new ActionAsPdf(pdfaction, model)
            {
                FileName = Path
            };
        }

    }
}
