using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BebachModel;
using Bebach.Extensions.Toastr;
using Bebach.Logging;

namespace Bebach.Extensions
{
    public class BulkInsertHelper
    {
        private static ILogger _logger = new Logger();
        public static bool  UnesiDnevneAktivnosti(int bebaID) {
            Aktivnost initAkt = new Aktivnost();
            try
            {
                //PRVO UNOSIMO hranjenje, presvlačenje
                initAkt.BebaID = bebaID;
                initAkt.Cijena = 0.00m;
                initAkt.Datum = DateTime.Now;
                initAkt.Opis = "";
                initAkt.VrstaID = 1;
                initAkt.Trajanje = 0;
                //initAkt.TrajanjeOd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 2, DateTime.Now.Minute, DateTime.Now.Second);
                //initAkt.TrajanjeDo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 2, DateTime.Now.Minute + 10, DateTime.Now.Second);
            }
            catch (DataException)
            {
                _logger.Error("Dogodila se pogreška prilikom spremanja nove aktivnosti.");
                return false;
            }
            return true;
        }

    }
}