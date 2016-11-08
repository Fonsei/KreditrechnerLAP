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

        [Required]
        [Range(3000,40000)]
        [DefaultValue(3000)]
        public int Kreditbetrag { get; set; }

        [Required]
        [Range(12,60)]
        [DefaultValue(12)]
        public int Laufzeit { get; set; }

        [Required]
        public double RateMonatlich { get; set; }


    }
}