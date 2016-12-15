using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KreditrechnerLAP.logic
{
    public class KreditInstitut
    {
        public static Kunde ErzeugeKunde()
        {
            Debug.WriteLine("KreditInstitut - ErzeugeKunde");
            Debug.Indent();

            Kunde neuerKunde = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    neuerKunde = new logic.Kunde()
                    {
                        Vorname = "anonym",
                        Nachname = "anonym"
                        //Gechlecht = "x"
                    };
                    context.AlleKunden.Add(neuerKunde);

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    Debug.WriteLine($"{anzahlZeilenBetroffen} Kunden angelegt!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in ErzeugeKunde");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return neuerKunde;
        }

        

        /// <summary>
        /// Speichert zu einer übergebenene ID_Kunde den Wunsch Kredit und dessen Laufzeit ab
        /// </summary>
        /// <param name="kreditBetrag">die Höhe des gewünschten Kredits</param>
        /// <param name="laufzeit">die Laufzeit des gewünschten Kredits</param>
        /// <param name="idKunde">die ID des Kunden zu dem die Angaben gespeichert werden sollen</param>
        /// <returns>true wenn Eintragung gespeichert werden konnte und der Kunde existiert, ansonsten false</returns>
        public static bool KreditRahmenSpeichern(double kreditBetrag, int laufzeit, int idKunde)
        {
            Debug.WriteLine("KreditInstitut - KreditRahmenSpeichern");
            Debug.Indent();

            bool erfolgreich = false;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {

                    /// speichere zum Kunden die Angaben
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {
                        Kredit neuerKreditWunsch = new Kredit()
                        {
                            Betrag = (decimal)kreditBetrag,
                            Zeitraum = laufzeit,
                            ID = idKunde
                        };

                        context.AlleKredite.Add(neuerKreditWunsch);
                    }

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 1;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} KreditRahmen gespeichert!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KreditRahmenSpeichern");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;
        }

        public static Kredit KreditRahmenLaden(int id)
        {
            Debug.WriteLine("KreditInstitut - KreditRahmenLaden");
            Debug.Indent();

            Kredit wunsch = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    wunsch = context.AlleKredite.Where(x => x.ID == id).FirstOrDefault();
                    Debug.WriteLine("KreditRahmen geladen!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KreditRahmenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return wunsch;
        }

        /// <summary>
        /// Speichert die Daten aus der Finanziellen Situation zu einem Kunden
        /// </summary>
        /// <param name="nettoEinkommen">das Netto Einkommen des Kunden</param>
        /// <param name="ratenVerpflichtungen">Raten Verpflichtungen des Kunden</param>
        /// <param name="wohnkosten">die Wohnkosten des Kunden</param>
        /// <param name="einkünfteAlimenteUnterhalt">Einkünfte aus Alimente und Unterhalt</param>
        /// <param name="unterhaltsZahlungen">Zahlungen für Alimente und Unterhalt</param>
        /// <param name="idKunde">die id des Kunden</param>
        /// <returns>true wenn die finanzielle Situation erfolgreich gespeichert werden konnte, ansonsten false</returns>
        public static bool FinanziellesSpeichern(double nettoEinkommen, double ratenVerpflichtungen, double wohnkosten, double einkünfteAlimenteUnterhalt, double unterhaltsZahlungen, int idKunde)
        {
            Debug.WriteLine("KreditInstitut - FinanzielleSituationSpeichern");
            Debug.Indent();

            bool erfolgreich = false;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {

                    /// speichere zum Kunden die Angaben
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {
                        FinanzielleSituation neueFinanzielleSituation = new FinanzielleSituation()
                        {
                            
                            NettoEinkommen = (decimal)nettoEinkommen,
                            Unterhaltszahlung = (decimal)unterhaltsZahlungen,
                            EinkünfteAlimente = (decimal)einkünfteAlimenteUnterhalt,
                            Wohnkosten = (decimal)wohnkosten,
                            Ratenverpflichtung = (decimal)ratenVerpflichtungen,
                            ID = idKunde
                        };

                        context.AlleFinanzielleSituationen.Add(neueFinanzielleSituation);
                    }

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 1;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} FinanzielleSituation gespeichert!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in FinanzielleSituation");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;
        }

        /// <summary>
        /// Liefert alle Beschäftigungsarten zurück
        /// </summary>
        /// <returns>alle Beschäftigungsarten oder null bei einem fehler</returns>
        public static List<Beschaeftigungsart> BeschaeftigungsartenLaden()
        {
            Debug.WriteLine("KreditInstitut - BeschaeftigungsartenLaden");
            Debug.Indent();
                List<Beschaeftigungsart>  alleBeschaeftigungen = null;

                try
                {
                    using (var context = new dbKreditInstitutEntities())
                    {
                        alleBeschaeftigungen = context.AlleBeschaeftigungsart.ToList();
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("Fehler in BeschaeftigungsartenLaden");
                    Debug.Indent();
                    Debug.WriteLine(ex.Message);
                    Debug.Unindent();
                    Debugger.Break();
                }


            Debug.Unindent();
            return alleBeschaeftigungen;
        }

        

        /// <summary>
        /// Liefert alle Branchen zurück
        /// </summary>
        /// <returns>alle Branchen oder null bei einem fehler</returns>
        public static List<Branche> BranchenAngabenLaden()
        {
            Debug.WriteLine("KreditInstitut - BranchenAngabenLaden");
            Debug.Indent();
            List<Branche> alleBranchen = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleBranchen = context.AlleBranchen.ToList();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Fehler in BeschaeftigungsartenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }


            Debug.Unindent();
            return alleBranchen;
        }

        /// <summary>
        /// Liefert alle Schulabschlüsse zurück
        /// </summary>
        /// <returns>alle Schulabschlüsse oder null bei einem Fehler</returns>
        public static List<Ausbildung> BildungsAngabenLaden()
        {
            Debug.WriteLine("KreditInstitut - BildungsAngabenLaden");
            Debug.Indent();

            List<Ausbildung> alleAusbildungen = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleAusbildungen = context.AlleAusbildungen.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in BildungsAngabenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleAusbildungen;
        }

        /// <summary>
        /// Liefert alle FamilienStand zurück
        /// </summary>
        /// <returns>alle FamilienStand oder null bei einem Fehler</returns>
        public static List<Familienstand> FamilienStandAngabenLaden()
        {
            Debug.WriteLine("KreditInstitut - FamilienStandAngabenLaden");
            Debug.Indent();

            List<Familienstand> alleFamilienStandsAngaben = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleFamilienStandsAngaben = context.AlleFamilienStandsAngaben.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in FamilienStandAngabenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleFamilienStandsAngaben;
        }

        /// <summary>
        /// Liefert alle Länder zurück
        /// </summary>
        /// <returns>alle Länder oder null bei einem Fehler</returns>
        public static List<Land> LaenderLaden()
        {
            Debug.WriteLine("KreditInstitut - LaenderLaden");
            Debug.Indent();

            List<Land> alleLaender = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleLaender = context.AlleLänder.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in LaenderLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleLaender;
        }

        

        /// <summary>
        /// Liefert alle Wohnarten zurück
        /// </summary>
        /// <returns>alle Wohnarten oder null bei einem Fehler</returns>
        public static List<Wohnart> WohnartenLaden()
        {
            Debug.WriteLine("KreditInstitut - WohnartenLaden");
            Debug.Indent();

            List<Wohnart> alleWohnarten = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleWohnarten = context.AlleWohnarten.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in WohnartenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleWohnarten;
        }

        /// <summary>
        /// Liefert alle IdentifikatikonsArt zurück
        /// </summary>
        /// <returns>alle IdentifikatikonsArt oder null bei einem Fehler</returns>
        public static List<IdentifikationsArt> IdentifikiationsAngabenLaden()
        {
            Debug.WriteLine("KreditInstitut - IdentifikiationsAngabenLaden");
            Debug.Indent();

            List<IdentifikationsArt> alleIdentifikationsArten = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleIdentifikationsArten = context.AlleIdentifikationsArten.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in IdentifikiationsAngabenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleIdentifikationsArten;
        }

        





        /// <summary>
        /// Liefert alle Titel zurück
        /// </summary>
        /// <returns>alle Titel oder null bei einem Fehler</returns>
        public static List<Titel> TitelLaden()
        {
            Debug.WriteLine("KreditInstitut - TitelLaden");
            Debug.Indent();

            List<Titel> alleTitel = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleTitel = context.AlleTitel.ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in TitelLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return alleTitel;
        }

        public static Konto KontoInformationenLaden(int id_Kunde)
        {
            Debug.WriteLine("KreditInstitut - KontoInformationenLaden");
            Debug.Indent();

            Konto kontoDaten = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    kontoDaten = context.AlleKonto.Where(x => x.ID == id_Kunde).FirstOrDefault();
                    Debug.WriteLine("KontoInformationen geladen!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KontoInformationenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return kontoDaten;
        }

        public static bool PersoenlicheDatenSpeichern(int? idTitel, string geschlecht, DateTime geburtsDatum, string vorname, string nachname, int? idTitelNachstehend, int idBildung, int idFamilienstand, int idIdentifikationsart, string identifikationsNummer, string idStaatsbuergerschaft, int idWohnart, int idKunde)
        {
            Debug.WriteLine("KreditInstitut - PersönlicheDatenSpeichern");
            Debug.Indent();

            bool erfolgreich = false;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {

                    /// speichere zum Kunden die Angaben
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {
                        aktKunde.Vorname = vorname;
                        aktKunde.Nachname = nachname;
                        aktKunde.Geburtsdatum = geburtsDatum;
                        aktKunde.FKFamilienstand = idFamilienstand;
                        aktKunde.FKAusbildung = idBildung;
                        aktKunde.FKStaatsbuergerschaft = idStaatsbuergerschaft;
                        aktKunde.FKTitel = idTitel;
                        aktKunde.FKIdentifikationsArt = idIdentifikationsart;
                        aktKunde.Idendifikationsnummer = identifikationsNummer;
                        aktKunde.Geschlecht = geschlecht;
                        aktKunde.FKWohnart = idWohnart;
                    }

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 0;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} PersönlicheDaten gespeichert!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in PersönlicheDatenSpeichern");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;
        }

        public static Kunde PersoenlicheDatenLaden(int id)
        {
            Debug.WriteLine("KreditInstitut - PersönlicheDatenLaden");
            Debug.Indent();

            Kunde persönlicheDaten = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    persönlicheDaten = context.AlleKunden.Where(x => x.ID == id).FirstOrDefault();
                    Debug.WriteLine("PersönlicheDatenLaden geladen!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in PersönlicheDatenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return persönlicheDaten;
        }

        public static Kunde KundeLaden(int idKunde)
        {
            Debug.WriteLine("KreditInstitut - KundeLaden");
            Debug.Indent();

            Kunde aktuellerKunde = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    aktuellerKunde = context.AlleKunden
                        .Include("Arbeitgeber")
                        .Include("Arbeitgeber.Beschaeftigungsart")
                        .Include("Arbeitgeber.Branche")
                        .Include("Familienstand")
                        .Include("FinanzielleSituation")
                        .Include("IdentifikationsArt")
                        .Include("Kontaktdaten")
                        .Include("Konto")
                        .Include("Kredit")
                        .Include("Ausbildung")
                        .Include("Titel")
                        .Include("Wohnart")
                        .Include("Staatsbuerger")
                        .Where(x => x.ID == idKunde).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KundeLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return aktuellerKunde;
        }

        public static bool ArbeitgeberDatenSpeichern(string arbeitgeberName, int beschaeftigungsArt, int branche, DateTime beschaeftigtSeit, int idKunde)
        {
            Debug.WriteLine("KreditInstitut - ArbeitgeberDatenSpeichern");
            Debug.Indent();
            bool erfolgreich = false;
                try
                {
                    using(var context = new dbKreditInstitutEntities())
                    {
                    /// speichere zum Kunden die Angaben
                    //Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {
                        Arbeitgeber neuerArbeitgeber = new Arbeitgeber()
                        {
                            BeschaeftigtSeit = beschaeftigtSeit,
                            FKBranche = branche,
                            FKBeschaeftigungsart = beschaeftigungsArt,
                            Firmenname = arbeitgeberName
                        };
                        aktKunde.Arbeitgeber = neuerArbeitgeber;
                    }
                   
                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 0;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} Arbeitgeber gespeichert!");
                    }
                }
                catch (Exception ex)
                {

                    Debug.WriteLine("Fehler in BildungsAngabenLaden");
                    Debug.Indent();
                    Debug.WriteLine(ex.Message);
                    Debug.Unindent();
                    Debugger.Break();
                }

            Debug.Unindent();
            return erfolgreich;
        }

        public static Arbeitgeber ArbeitgeberAngabenLaden(int idKunde)
        {
            Debug.WriteLine("KreditInstitut - ArbeitgeberAngabenLaden");
            Debug.Indent();

            Arbeitgeber arbeitGeber = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    arbeitGeber = context.AlleArbeitgeber.Where(x => x.ID == idKunde).FirstOrDefault();
                    Debug.WriteLine("ArbeitgeberAngaben geladen!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in ArbeitgeberAngabenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return arbeitGeber;
        }


        public static bool KontaktDatenSpeichern(string strasse, string hausnummer, string stiege, string tuer, int fkort, string email, string telefonnummer, int idKunde)
        {

            bool erfolgreich = false;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {

                    /// speichere zum Kunden die Angaben
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {


                        Kontaktdaten kontakt = new Kontaktdaten() {
                            Strasse = strasse,
                            Hausnummer = hausnummer,
                            Stiege =stiege,
                            Tuer = tuer,
                            FKOrt = fkort,
                            Telefonnummer = telefonnummer,
                            EMail = email
                    };
                        aktKunde.Kontaktdaten = kontakt;

                    }

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 0;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} KontaktDaten gespeichert!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KontaktDatenSpeichern");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;

        }

        public static Kontaktdaten KontaktDatenLaden(int idKunde)
        {
            Debug.WriteLine("KreditInstitut - KontaktDatenLaden");
            Debug.Indent();

            Kontaktdaten daten = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    daten = context.AlleKontaktdaten.Where(x => x.ID == idKunde).FirstOrDefault();
                    Debug.WriteLine("KontaktDaten geladen!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KontaktDatenLaden");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return daten;
        }

        public static bool KontoInformationenSpeichern(string iban, string bic, string bankname, int idKunde)
        {

            bool erfolgreich = false;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {

                    /// speichere zum Kunden die Angaben
                    Kunde aktKunde = context.AlleKunden.Where(x => x.ID == idKunde).FirstOrDefault();

                    if (aktKunde != null)
                    {
                        Konto kontoinfo = new Konto()
                        {
                            Bankname = bankname,
                            IBAN = iban,
                            BIC = bic
                        };

                        aktKunde.Konto = kontoinfo;

                        

                    }

                    int anzahlZeilenBetroffen = context.SaveChanges();
                    erfolgreich = anzahlZeilenBetroffen >= 0;
                    Debug.WriteLine($"{anzahlZeilenBetroffen} KontoInformationen gespeichert!");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Fehler in KontoInformationenSpeichern");
                Debug.Indent();
                Debug.WriteLine(ex.Message);
                Debug.Unindent();
                Debugger.Break();
            }

            Debug.Unindent();
            return erfolgreich;

        }


       

        /// <summary>
        /// Ladet alle Ort und gibt siwe zurück
        /// </summary>
        /// <returns>Gibt eine Liste von Orten zurück</returns>
        public static List<Ort> LadeAlleOrte()
        {
            Debug.WriteLine("KreditInstitut - LadeAlleOrte");
            Debug.Indent();

            List<Ort> alleOrte = null;

            using(var context = new dbKreditInstitutEntities())
            {
                alleOrte = context.AlleOrt.ToList();
            }
            Debug.Unindent();
            return alleOrte;
        }



    }
}