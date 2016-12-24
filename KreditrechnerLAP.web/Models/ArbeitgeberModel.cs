using KreditrechnerLAP.logic;
using KreditrechnerLAP.web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace onlineKredit.web.Models
{
    public class ArbeitgeberModel
    {

        [Required]
        [Display(Name = "Arbeitgeber")]
        public string Arbeitgeber { get; set; }

        [Required]
        [Display(Name = "Beschäftigungsart")]
        public int ID_Beschaeftigungsart { get; set; }

        [Required]
        [Display(Name = "Branche")]
        public int ID_Branche { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Angestellt seit")]
        public DateTime Beschaeftigtseit { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }

        public List<BeschaeftigungsArtModel> AlleBeschaeftigungsarten { get; set; }
        public List<BrancheModel> AlleBranchen { get; set; }


    }
}