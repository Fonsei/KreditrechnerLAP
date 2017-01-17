using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KreditrechnerLAP.web.Models
{
    public class AnmeldeModel
    {

        [Required]
        [Display(Name = "Benutzername:")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }


    }
}