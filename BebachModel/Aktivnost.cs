//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BebachModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Aktivnost
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Datum { get; set; }
        public string Opis { get; set; }
        public System.DateTime TrajanjeOd { get; set; }
        public System.DateTime TrajanjeDo { get; set; }
        public int BebaID { get; set; }
        public int VrstaID { get; set; }
        public Nullable<decimal> Cijena { get; set; }
    
        public virtual Beba Beba { get; set; }
        public virtual VrstaAkt VrstaAkt { get; set; }
    }
}