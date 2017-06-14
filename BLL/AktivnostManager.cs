using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BebachModel;

namespace BLL
{
    public class AktivnostManager
    {

        private static Entities db = new Entities();
        public static int AutoKreiraj(Aktivnost akt)
        {

            DateTime pom = DateTime.Now.Date;
            DateTime datOd = new DateTime(pom.Year, pom.Month, pom.Day, 0, 0, 0);
            int bebaID = akt.BebaID;
            DateTime datBebe = db.Bebas.First(s => s.ID == bebaID).Dat_rod.GetValueOrDefault(DateTime.MinValue);
            double mjeseci = 0;
            Aktivnost insert = new Aktivnost();
            // po mjesecima radimo algoritam
            if (datBebe != DateTime.MinValue)
            {
                mjeseci = datOd.Subtract(datBebe).Days / (365.25 / 12);
                if (mjeseci < 12.00)
                {


                    #region Unos aktivnosti
                    //ovisno o mjesecima napraviti unos aktivnosti za jedan dan
                    var aktivnosti = new List<Aktivnost>
                {
                    // prvo za dojenje, uzima se razmak od 2h
                    new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                      new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                        new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                          new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                            new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                              new Aktivnost {Datum = datOd.Date, Opis = "Dojenje",Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                               new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
                                new Aktivnost {Datum = datOd.Date, Opis = "Dojenje", Trajanje = 5, BebaID = bebaID, VrstaID = 1, Cijena = 0.00m},
// unos za igranje, prvo igranje je ujutro
                    new Aktivnost {Datum = datOd.Date, Opis = "Igranje", Trajanje = 20, BebaID = bebaID, VrstaID = 6, Cijena = 0.00m},
                    new Aktivnost {Datum = datOd.Date, Opis = "Igranje",  Trajanje = 20, BebaID = bebaID, VrstaID = 6, Cijena = 0.00m},
                    new Aktivnost {Datum = datOd.Date, Opis = "Igranje",  Trajanje = 20, BebaID = bebaID, VrstaID = 6, Cijena = 0.00m},
                    new Aktivnost {Datum = datOd.Date, Opis = "Igranje",  Trajanje = 20, BebaID = bebaID, VrstaID = 6, Cijena = 0.00m},
                    new Aktivnost {Datum = datOd.Date, Opis = "Igranje",  Trajanje = 20, BebaID = bebaID, VrstaID = 6, Cijena = 0.00m},

                    // unos za šetanje
                    new Aktivnost {Datum = datOd.Date, Opis = "Šetanje", Trajanje = 120, BebaID = bebaID, VrstaID = 5, Cijena = 0.00m},

                    // unos za presvlačenje
                     new Aktivnost {Datum = datOd.Date, Opis = "Presvlačenje",  Trajanje = 5, BebaID = bebaID, VrstaID = 10, Cijena = 0.00m},
                      new Aktivnost {Datum = datOd.Date, Opis = "Presvlačenje",  Trajanje = 5, BebaID = bebaID, VrstaID = 10, Cijena = 0.00m},
                       new Aktivnost {Datum = datOd.Date, Opis = "Presvlačenje", Trajanje = 5, BebaID = bebaID, VrstaID = 10, Cijena = 0.00m},
                        new Aktivnost {Datum = datOd.Date, Opis = "Presvlačenje",  Trajanje = 5, BebaID = bebaID, VrstaID = 10, Cijena = 0.00m},

                };
                    #endregion

                    aktivnosti.ForEach(s => db.Aktivnosts.Add(s));
                    db.SaveChanges();

                    return 1;
                }
                else
                {
                    // ako nemamo datum rođenja onda uzmi 12 kao default
                    return 1;

                }

            }
            else
            {
                return 2;
            }
        }
    }
}