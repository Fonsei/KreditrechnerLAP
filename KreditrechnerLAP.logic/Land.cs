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
    
    public partial class Land
    {
        public Land()
        {
            this.AlleKunde = new HashSet<Kunden>();
            this.AlleOrt = new HashSet<Ort>();
        }
    
        public string ID { get; set; }
        public string Bezeichnung { get; set; }
    
        public virtual ICollection<Kunden> AlleKunde { get; set; }
        public virtual ICollection<Ort> AlleOrt { get; set; }
    }
}
