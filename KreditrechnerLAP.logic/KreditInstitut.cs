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
            Debug.WriteLine("KonsumKreditVerwaltung - ErzeugeKunde");
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
            Debug.WriteLine("KonsumKreditVerwaltung - KreditRahmenSpeichern");
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
        public static bool FinanzielleSituationSpeichern(double nettoEinkommen, double ratenVerpflichtungen, double wohnkosten, double einkünfteAlimenteUnterhalt, double unterhaltsZahlungen, int idKunde)
        {
            Debug.WriteLine("KonsumKreditVerwaltung - FinanzielleSituationSpeichern");
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
        /// Liefert alle Schulabschlüsse zurück
        /// </summary>
        /// <returns>alle Schulabschlüsse oder null bei einem Fehler</returns>
        public static List<Ausbildung> BildungsAngabenLaden()
        {
            Debug.WriteLine("KonsumKreditVerwaltung - BildungsAngabenLaden");
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
            Debug.WriteLine("KonsumKreditVerwaltung - FamilienStandAngabenLaden");
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
            Debug.WriteLine("KonsumKreditVerwaltung - LaenderLaden");
            Debug.Indent();

            List<Land> alleLänder = null;

            try
            {
                using (var context = new dbKreditInstitutEntities())
                {
                    alleLänder = context.AlleLänder.ToList();
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
            return alleLänder;
        }

        /// <summary>
        /// Liefert alle Wohnarten zurück
        /// </summary>
        /// <returns>alle Wohnarten oder null bei einem Fehler</returns>
        public static List<Wohnart> WohnartenLaden()
        {
            Debug.WriteLine("KonsumKreditVerwaltung - WohnartenLaden");
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
            Debug.WriteLine("KonsumKreditVerwaltung - IdentifikiationsAngabenLaden");
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
            Debug.WriteLine("KonsumKreditVerwaltung - TitelLaden");
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

        

        public static bool PersönlicheDatenSpeichern(int? idTitel, string geschlecht, DateTime geburtsDatum, string vorname, string nachname, int? idTitelNachstehend, int idBildung, int idFamilienstand, int idIdentifikationsart, string identifikationsNummer, string idStaatsbuergerschaft, int idWohnart, int idKunde)
        {
            Debug.WriteLine("KonsumKreditVerwaltung - PersönlicheDatenSpeichern");
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
                    erfolgreich = anzahlZeilenBetroffen >= 1;
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
    }
}