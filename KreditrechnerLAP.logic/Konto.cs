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
    
    public partial class Konto
    {
        public int ID { get; set; }
        public Nullable<bool> KontoNeu { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string Bankname { get; set; }
    
        public virtual Kunden AlleKunde { get; set; }
    }
}
