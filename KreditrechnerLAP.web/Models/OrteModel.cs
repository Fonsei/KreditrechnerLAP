using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KreditrechnerLAP.web.Models
{
    public class OrteModel
    {

        public int ID { get; set; }

        public string PLZ { get; set; }

        public string Bezeichnung { get; set; }


        public string Anzeige {
            get
            {
                return $"({PLZ}) {Bezeichnung}";
            }

        }


    }
}