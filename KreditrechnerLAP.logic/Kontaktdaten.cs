//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KreditrechnerLAP.logic
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kontaktdaten
    {
        public int ID { get; set; }
        public Nullable<int> FKOrt { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Stiege { get; set; }
        public string Tuer { get; set; }
        public string EMail { get; set; }
        public string Telefonnummer { get; set; }
    
        public virtual Ort AlleOrt { get; set; }
        public virtual Kunden AlleKunde { get; set; }
    }
}
