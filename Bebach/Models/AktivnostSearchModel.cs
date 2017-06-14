using BebachModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bebach.Models
{
    public class AktivnostSearchModel
    {
        public DateTime? datOd { get; set; }
        public DateTime? datDo { get; set; }
        public int? bebaID { get; set; }
        public int? Vrsta { get; set; }

        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public String Sort { get; set; }
        public String SortDir { get; set; }
        public Int32 TotalRecords { get; set; }
        public List<Aktivnost> Aktivnosts { get; set; }

        public AktivnostSearchModel()
        {
            Page = 1;
            PageSize = 5;
            Sort = "Datum";
            SortDir = "DESC";
        }
    }
}