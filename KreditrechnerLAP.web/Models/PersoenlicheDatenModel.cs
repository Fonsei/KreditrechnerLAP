using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KreditrechnerLAP.web.Models
{
    //public enum Geschlecht
    //{
    //    Männlich,
    //    Weiblich
    //}
    public class PersoenlicheDatenModel
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Geschlecht")]
        [EnumDataType(typeof(Geschlecht))]
        public Geschlecht Geschlecht { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Vorname", Prompt = "Max")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Nachname", Prompt = "Mustermann")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Geburtsdatum", Prompt = "1988-08-08")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Geburtsdatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Familienstand", Prompt = "Verheiratet")]
        public string Familienstand { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Staatsbürger", Prompt = "Österreich")]
        public string Staatsbuerger { get; set; }


    }
}