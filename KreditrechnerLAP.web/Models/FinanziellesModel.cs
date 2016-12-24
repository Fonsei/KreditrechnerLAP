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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte eine Zahl eingeben")]
        [Range(500, 10000, ErrorMessage = "Wert muss zwischen 500 und 10000 liegen")]
        [Display(Name = "Monatliches Netto-Einkommen")]
        public double NettoEinkommen { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte eine Zahl eingeben")]
        [Range(0, 10000, ErrorMessage = "Wert muss zwischen 0 und 10000 liegen")]
        [Display(Name = "Wohnkosten (Miete, Heizung, Strom)")]
        public double Wohnkosten { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte eine Zahl eingeben")]
        [Range(0, 10000, ErrorMessage = "Wert muss zwischen 0 und 10000 liegen")]
        [Display(Name = "Einkünfte aus Alimenten, Unterhalte usw.")]
        public double EinkünfteAlimente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte eine Zahl eingeben")]
        [Range(0, 10000, ErrorMessage = "Wert muss zwischen 0 und 10000 liegen")]
        [Display(Name = "Zahlungen für Unterhalt, Alimente usw.")]
        public double Unterhalt { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Pflichtfeld")]
        [DataType(DataType.Currency, ErrorMessage = "Bitte eine Zahl eingeben")]
        [Range(0, 10000, ErrorMessage = "Wert muss zwischen 0 und 10000 liegen")]
        [Display(Name = "Raten-Verpflichtungen")]
        public double RatenVerpflichtung { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        public int ID_Kunde { get; set; }


    }
}