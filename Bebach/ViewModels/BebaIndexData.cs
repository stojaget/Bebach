using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BebachModel;
namespace Bebach.ViewModels
{
    public class BebaIndexData
    {
        public IEnumerable<Beba> Bebas { get; set; }
        public IEnumerable<Aktivnost> Aktivnosts { get; set; }
       // public IEnumerable<VrstaAkt> VrstaAkts { get; set; }
        public IEnumerable<Pregled> Pregleds { get; set; }
        public IEnumerable<Slika> Slikas { get; set;}
       // public IEnumerable<BebaSlika> BebaSlikas { get; set; }
    }
}