using BebachModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bebach.Controllers
{
    public class AktivnostDragNDropController : ApiController
    {
        private Entities db = new Entities();

        /// <summary>
        /// napraviti će kreiranje aktivnosti za jedan dan, vraća 1 ako sve uspije, 2 ako se desi greška
        /// </summary>
        /// <param name="akt"></param>
        /// <returns></returns>
        public int KreirajAuto(Aktivnost akt)
        {
            return 1;
        }

        [Route("Aktivnosti")]
        public IEnumerable<Aktivnost> GetAkts()
        {
            return db.Aktivnosts.ToList();
        }
    }
}
