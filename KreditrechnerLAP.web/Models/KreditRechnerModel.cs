using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KreditrechnerLAP.web.Models
{
    public class KreditRechnerModel
    {


        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Gewünschter Betrag")]
        [Range(3000,40000,ErrorMessage = "Betrag muss zwischen 3.000€ und 40.000€ sein")]
        public int Kreditbetrag { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Gewünschte Rate")]
        [Range(12, 120, ErrorMessage = "Rate muss zwichen 12 und 120 Monate sein")]
        public int Zeitraum { get; set; }

        
        public double Zinsen { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }


    }
}