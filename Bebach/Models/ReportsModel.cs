using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bebach.Models
{
    public class SearchParameterModel
    {

        [Display(Name = "Traženje po datumu")]
        public DateTime DatumOd
        {
            get;

            set;
        }

        public DateTime DatumDo
        {
            get;

            set;
        }

        public int BebaID
        {
            get;
            set;
        }

        public string Format
        {
            get;

            set;
        }

        public Vrsta TipIzvjesca { get; set; }

        public enum Vrsta
        {
            [Display(Name = "Izaberite vrijednost")]
            OdaberiteNesto,
            [Display(Name = "Pregled aktivnosti")]
            Aktivnosti,
            [Display(Name = "Izvješće o pregledima")]
            Pregledi
        }

        public class AktivnostModel
        {

            private DateTime m_Datum;
            private string m_Opis;
            private DateTime m_TrajanjeOd;
            private DateTime m_TrajanjeDo;
            private decimal m_Cijena;
            private string m_Vrsta;
            private string m_Beba;

            public DateTime Datum
            {
                get { return m_Datum; }

                set { value = m_Datum; }
            }

            public string Opis
            {
                get { return m_Opis; }

                set { value = m_Opis; }
            }

            public DateTime TrajanjeOd
            {
                get { return m_TrajanjeOd; }

                set { value = m_TrajanjeOd; }
            }

            public DateTime TrajanjeDo
            {
                get { return m_TrajanjeDo; }

                set { value = m_TrajanjeDo; }


            }

            public decimal Cijena
            {
                get { return m_Cijena; }

                set { value = m_Cijena; }


            }

            public string Vrsta
            {
                get { return m_Vrsta; }

                set { value = m_Vrsta; }


            }

            public string Beba
            {
                get { return m_Beba; }

                set { value = m_Beba; }


            }


            public AktivnostModel() { }

            public AktivnostModel(DateTime datum, string opis, DateTime trajOd, DateTime trajDo, decimal cijena, string vrsta, string beba)
            {
                m_Datum = datum;
                m_Opis = opis;
                m_TrajanjeOd = trajOd;
                m_TrajanjeDo = trajDo;
                m_Cijena = cijena;
                m_Vrsta = vrsta;
                m_Beba = beba;
            }
        }


    }
}