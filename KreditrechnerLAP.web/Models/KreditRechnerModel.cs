using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KreditrechnerLAP.web.Models
{
    public class KreditRechnerModel
    {
        [Required]
        public int ID { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Gewünschter Betrag")]
        [Range(3000,40000,ErrorMessage = "Betrag muss zwischen 3.000€ und 40.000€ sein")]
        public int Kreditbetrag { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Gewünschter Betrag")]
        [Range(3000, 40000, ErrorMessage = "Betrag muss zwischen 3.000€ und 40.000€ sein")]
        public int Laufzeit { get; set; }

        [Required]
        public double RateMonatlich { get; set; }


    }
}