using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BebachModel
{
    public class BebaMetadata
    {
        public int ID { get; set; }
        [StringLength(100)]
        public string Ime { get; set; }
        [StringLength(100)]
        public string Prezime { get; set; }
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum rođenja")]
        public Nullable<System.DateTime> Dat_rod { get; set; }
        [StringLength(11)]
        public string OIB { get; set; }
        public string Adresa { get; set; }
        public string Majka { get; set; }
        public string Otac { get; set; }

        public string Racun { get; set; }
        [DataType(DataType.Date)]
        [Editable(false)]
        public Nullable<System.DateTime> Dat_kreiranja { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Datum ažuriranja")]
        public Nullable<System.DateTime> Dat_azu { get; set; }
        public string Unio { get; set; }
        public Nullable<bool> Aktivan { get; set; }



        public virtual ICollection<Aktivnost> Aktivnosts { get; set; }


        public virtual ICollection<Pregled> Pregleds { get; set; }


    }

    public class AktivnostMetadata
    {
        public int ID { get; set; }
        [DataType(DataType.Date)]

        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        // [UIHint("CustomDate")]
        [Display(Name = "Datum")]
        public Nullable<System.DateTime> Datum { get; set; }
        public string Opis { get; set; }
        public int Trajanje { get; set; }
        //[DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
         //[UIHint("CustomTime")]
       // public System.DateTime TrajanjeOd { get; set; }
        //[DataType(DataType.Time)]
       // [UIHint("CustomTime")]
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:HH:mm}")]
        //public System.DateTime TrajanjeDo { get; set; }
        public int BebaID { get; set; }
        public int VrstaID { get; set; }

        [DataType(DataType.Currency)]
        public Nullable<decimal> Cijena { get; set; }

        public virtual Beba Beba { get; set; }
        public virtual VrstaAkt VrstaAkt { get; set; }
    }

}
