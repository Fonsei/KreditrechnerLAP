using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KreditrechnerLAP.web.Models
{
    public class FinanziellesModel
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Monatliches Netto Einkommen")]
        public double NettoEinkommen { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Wohnkosten(Miete,Strom,Heizung)")]
        [DefaultValue(0)]
        public double Wohnkosten { get; set; }

        [Display(Name = "Aliemente die Sie bekommen")]
        [DefaultValue(0)]
        public double EinkünfteAlimente { get; set; }

        [Display(Name = "Zahlungen für Unterhalt, Alimente usw.")]
        [DefaultValue(0)]
        public double Unterhalt { get; set; } 


        [Display(Name = "Monatliche raten(Kredit)")]
        [DefaultValue(0)]
        public double RatenVerpflichtung { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }


    }
}