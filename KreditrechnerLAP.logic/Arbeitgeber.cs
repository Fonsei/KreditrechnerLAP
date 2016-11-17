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
    
    public partial class Arbeitgeber
    {
        public int ID { get; set; }
        public Nullable<int> FKBeschaeftigungsart { get; set; }
        public Nullable<int> FKBranche { get; set; }
        public string Firmenname { get; set; }
        public Nullable<System.DateTime> BeschaeftigtSeit { get; set; }
    
        public virtual Beschaeftigungsart AlleBeschaeftigungsart { get; set; }
        public virtual Branche AlleBranche { get; set; }
        public virtual Kunden AlleKunde { get; set; }
    }
}
