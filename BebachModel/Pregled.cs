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
    
    public partial class Pregled
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> Datum { get; set; }
        public string Opis { get; set; }
        public Nullable<decimal> Visina { get; set; }
        public Nullable<decimal> Tezina { get; set; }
        public string Bolesti { get; set; }
        public Nullable<decimal> OpsegGlave { get; set; }
        public Nullable<int> BebaID { get; set; }
    
        public virtual Beba Beba { get; set; }
    }
}