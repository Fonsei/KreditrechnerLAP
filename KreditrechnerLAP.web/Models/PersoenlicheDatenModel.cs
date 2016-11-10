using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KreditrechnerLAP.web.Models
{
    public enum Geschlecht
    {
        Männlich,
        Weiblich
    }
    public class PersoenlicheDatenModel
    {
        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Geschlecht")]
        public Geschlecht Geschlecht { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Vorname")]
        public string Vorname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Nachname")]
        public string Nachname { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Geburtsdatum")]
        public DateTime Geburtsdatum { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Familienstand")]
        public string Familienstand { get; set; }

        [Required(ErrorMessage = "Pflichtfeld")]
        [Display(Name = "Staatsbürger")]
        public string Staatsbuerger { get; set; }


    }
}