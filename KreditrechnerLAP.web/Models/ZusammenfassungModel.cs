using KreditrechnerLAP.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace onlineKredit.web.Models
{
    public class ZusammenfassungModel
    {


        #region Allgemein
        public int ID_Kunde { get; set; }
        #endregion

        #region KreditRahmen
        public int GewünschterBetrag { get; set; }
        public int Laufzeit { get; set; }
        #endregion

        #region Finanzielle Situation
        public double NettoEinkommen { get; set; }

        public double Wohnkosten { get; set; }

        public double EinkünfteAlimenteUnterhalt { get; set; }

        public double UnterhaltsZahlungen { get; set; }

        public double RatenVerpflichtungen { get; set; }
        #endregion

        #region Persönliche Daten
        public string Geschlecht { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Titel { get; set; }

        public string TitelNachstehend { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime GeburtsDatum { get; set; }

        public string Staatsbuergerschaft { get; set; }

        public int AnzahlUnterhaltspflichtigeKinder { get; set; }

        public string Familienstand { get; set; }

        public string Wohnart { get; set; }

        public string Bildung { get; set; }

        public string Identifikationsart { get; set; }

        public string IdentifikationsNummer { get; set; }
        #endregion

        #region Arbeitgeber
        public string FirmenName { get; set; }

        public string BeschäftigungsArt { get; set; }

        public string Branche { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}",
               ApplyFormatInEditMode = true)]
        public string BeschäftigtSeit { get; set; }
        #endregion

        #region KontaktDaten
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Ort { get; set; }
        public string PLZ { get; set; }
        public string Bezeichnung { get; set; }
        public string Mail { get; set; }
        public string TelefonNummer { get; set; }

        public string Anzeige
        {
            get
            {
                return $"({PLZ}) {Ort}";
            }
        }

        
        #endregion

        #region KontoInformationen
        public bool NeuesKonto { get; set; }

        public string BankName { get; set; }

        public string IBAN { get; set; }

        public string BIC { get; set; }
        #endregion
    }
}