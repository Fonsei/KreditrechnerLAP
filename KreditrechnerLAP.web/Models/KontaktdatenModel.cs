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
        public string Email { get; set; }

        [Required]
        [Display(Name = "Telefonnummer")]
        public string Telefonnummer { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }

        public List<OrteModel> AlleOrte { get; set; }

    }
}