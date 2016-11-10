using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [Display(Name = "Unterhalt den sie Erhalten")]
        [DefaultValue(0)]
        public double Unterhalt { get; set; } 


        [Display(Name = "Monatliche raten(Kredit)")]
        [DefaultValue(0)]
        public double RatenVerpflichtung { get; set; }


    }
}