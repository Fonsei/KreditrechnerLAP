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
    
    public partial class FinanzielleSituation
    {
        public int ID { get; set; }
        public Nullable<decimal> NettoEinkommen { get; set; }
        public Nullable<decimal> Wohnkosten { get; set; }
        public Nullable<decimal> EinkünfteAlimente { get; set; }
        public Nullable<decimal> Unterhaltszahlung { get; set; }
        public Nullable<decimal> Ratenverpflichtung { get; set; }
    
        public virtual Kunden AlleKunde { get; set; }
    }
}
