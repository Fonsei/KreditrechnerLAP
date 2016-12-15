using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KreditrechnerLAP.web.Models
{
    public class KontaktdatenModel
    {
        
        [Required]
        [Display(Name = "Straße")]
        public string Strasse { get; set; }

        [Required]
        [Display(Name = "Hausnummer")]
        public string Hausnummer { get; set; }


        [Display(Name = "Stiege")]
        public string Stiege { get; set; }


        [Display(Name = "Tür")]
        public string Tuer { get; set; }

        [Required]
        [Display(Name ="Ort")]
        public int ID_Ort { get; set; }

        [Required]
        [Display(Name = "Emailadresse")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email adresse nicht Richtig")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefonnummer")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Nur Zahlen Erlaubt")]
        public string Telefonnummer { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_PLZ { get; set; }

        public string PLZ { get; set; }

        public string Bezeichnung { get; set; }

        public string Anzeige
        {
            get
            {
                return $"({PLZ}) {Bezeichnung}";
            }
        }
        public List<OrteModel> AlleOrte { get; set; }

    }
}