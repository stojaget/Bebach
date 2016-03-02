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

namespace Bebach.Controllers
{
    public class AktivnostController : MessageControllerBase
    {
        private Entities db = new Entities();
        public DateTime datumOd = DateTime.Now.AddYears(-1);
        public DateTime datumDo = DateTime.Now;
        // GET: Aktivnost
        public ActionResult Search(int? bebaID, DateTime? datOd, DateTime? datDo)
        {
            if (bebaID != null)
            {
                if (datOd == null)
                {
                    datOd = DateTime.Now.AddMonths(-1);
                }
                if (datDo == null)
                {
                    datDo = DateTime.Now;
                }
                ViewBag.datOd = datOd.Value;
                ViewBag.datDo = datDo.Value;
                ViewBag.bebaID = bebaID.Value;
                var aktivnosts = db.Aktivnosts.Include(a => a.Beba).Include(a => a.VrstaAkt).Where(
                    i => i.BebaID == bebaID.Value).ToList();
                // napraviti sortiranje po ID, Datum poljima na aktivnosts


                var searchRes = aktivnosts.Where(p => p.Datum >= datOd && p.Datum <= datDo);

                return View("Index", searchRes.ToList());

            }
            else
            {
                return new EmptyResult();
            }




        }

        public ActionResult AutoKreiranje(int bebaID)
        {
            // kreiramo aktivnosti za jedan dan
            DateTime datOd = DateTime.Now;
            DateTime datDo = DateTime.Now.AddDays(1);
            DateTime datBebe = db.Bebas.First(s => s.ID == bebaID).Dat_rod.GetValueOrDefault(DateTime.MinValue);
            double mjeseci = 0;
            if (datBebe != DateTime.MinValue)
            {
                mjeseci = datOd.Subtract(datBebe).Days / (365.25 / 12);
            }
            else
            {
                // ako nemamo datum rođenja onda uzmi 12 kao default
                mjeseci = 12;
            }
            return new EmptyResult();
        }

        public ActionResult Index(int? bebaID, string sortOrder)
        {
            if (bebaID != null)
            {
                ViewBag.bebaID = bebaID.Value;
                var aktivnosts = db.Aktivnosts.Include(a => a.Beba).Include(a => a.VrstaAkt).Where(
                    i => i.BebaID == bebaID.Value).ToList();
                ViewBag.CijenaSortParm = String.IsNullOrEmpty(sortOrder) ? "cijena_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Datum" ? "date_desc" : "Datum";
                var sortirane = from s in aktivnosts
                                select s;
                switch (sortOrder)
                {
                    case "cijena_desc":
                        sortirane = sortirane.OrderByDescending(s => s.Cijena);
                        break;
                    case "Datum":
                        sortirane = sortirane.OrderBy(s => s.Datum);
                        break;
                    case "date_desc":
                        sortirane = sortirane.OrderByDescending(s => s.Datum);
                        break;
                    default:
                        sortirane = sortirane.OrderBy(s => s.Datum);
                        break;
                }
                return View(sortirane.ToList());
            }
            else
            {
                return new EmptyResult();
            }

        }

        //public JsonResult GetAktLists(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        //{

        //    int pageIndex = Convert.ToInt32(page) - 1;
        //    int pageSize = rows;
        //    var aktivnosts = db.Aktivnosts.Include(a => a.Beba).Include(a => a.VrstaAkt).Where(
        //      i => i.BebaID == beID).ToList();
        //    var todoListsResults = db.Aktivnosts.Select(
        //            a => new
        //            {
        //                a.ID,
        //                a.Opis,
        //                a.Datum,
        //                a.TrajanjeDo,
        //                a.TrajanjeOd,
        //                a.VrstaID,
        //                a.BebaID,
        //            });
        //    int totalRecords = todoListsResults.Count();
        //    var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
        //    if (sord.ToUpper() == "DESC")
        //    {
        //        todoListsResults = todoListsResults.OrderByDescending(s => s.Datum);
        //        todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
        //    }
        //    else
        //    {
        //        todoListsResults = todoListsResults.OrderBy(s => s.Datum);
        //        todoListsResults = todoListsResults.Skip(pageIndex * pageSize).Take(pageSize);
        //    }
        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page,
        //        records = totalRecords,
        //        rows = todoListsResults
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetVrsteAkt()
        //{

        //    var json = from vr in db.VrstaAkts select vr.ID + ":" + vr.Vrsta;

        //    var jsonData = new
        //    {
        //        //    rows = from vr in db.VrstaAkts.AsEnumerable()
        //        //           select new[] { vr.ID.ToString(),
        //        //              vr.Vrsta.ToString()}
        //        //};
        //        rows = (from vr in db.VrstaAkts.AsEnumerable()
        //                select new
        //                {
        //                    cell = new string[] {
        //                   vr.ID.ToString() ,
        //                   vr.Vrsta.ToString()
        //                    }
        //                }).ToArray()
        //    };
        //        //rows = (from vr in db.VrstaAkts
        //        //        select new
        //        //        {
        //        //            cell = new string[] {
        //        //           vr.ID.ToString(),
        //        //           vr.Vrsta.ToString()
        //        //       }
        //        //        })};

        //    return Json(json, JsonRequestBehavior.AllowGet);
        //}

        // GET: Aktivnost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktivnost aktivnost = db.Aktivnosts.Find(id);
            if (aktivnost == null)
            {
                return HttpNotFound();
            }
            return View(aktivnost);
        }

        // GET: Aktivnost/Create
        public ActionResult Create(int bebaID = 0)
        {
            ViewBag.BebaDropID = new SelectList(db.Bebas, "ID", "Ime");
            ViewBag.bebaID = bebaID;
            ViewBag.VrstaID = new SelectList(db.VrstaAkts, "ID", "Vrsta");
            Aktivnost initAkt = new Aktivnost();
            initAkt.BebaID = bebaID;
            initAkt.Cijena = 0.00m;
            initAkt.Datum = DateTime.Now;
            initAkt.Opis = string.Empty;
            initAkt.TrajanjeOd = DateTime.Now;
            initAkt.TrajanjeDo = DateTime.Now.AddMinutes(10.00);

            return View(initAkt);
        }

        // POST: Aktivnost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Datum,Opis,TrajanjeOd,TrajanjeDo,BebaID,VrstaID,Cijena")] Aktivnost aktivnost)
        {

            if (ModelState.IsValid)
            {
                if (aktivnost.Datum == null)
                {
                    aktivnost.Datum = DateTime.Now;
                }
                db.Aktivnosts.Add(aktivnost);
                db.SaveChanges();
                this.AddToastMessage("Uspješan unos", "Uspješno ste unijeli novu aktivnost", ToastType.Success);
                return RedirectToAction("Index", new { bebaID = aktivnost.BebaID });
            }
            else
            {

            }

            ViewBag.BebaDropID = new SelectList(db.Bebas, "ID", "Ime", aktivnost.BebaID);
            ViewBag.VrstaID = new SelectList(db.VrstaAkts, "ID", "Vrsta", aktivnost.VrstaID);
            return View(aktivnost);

        }

        // GET: Aktivnost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktivnost aktivnost = db.Aktivnosts.Find(id);
            if (aktivnost == null)
            {
                return HttpNotFound();
            }
            ViewBag.bebaID = new SelectList(db.Bebas, "ID", "Ime", aktivnost.BebaID);
            ViewBag.VrstaID = new SelectList(db.VrstaAkts, "ID", "Vrsta", aktivnost.VrstaID);
            return View(aktivnost);
        }

        // POST: Aktivnost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]

        public ActionResult EditAkt(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aktUpdate = db.Aktivnosts.Find(id);
            if (TryUpdateModel(aktUpdate, "", new string[] { "Datum", "Opis", "TrajanjeOd", "TrajanjeDo", "BebaID", "VrstaID", "Cijena" }))
            {
                try
                {
                    if (aktUpdate.Datum == null)
                    {
                        aktUpdate.Datum = DateTime.Now;
                    }

                    db.SaveChanges();
                    this.AddToastMessage("Uspješan unos", "Uspješno ste promijenili podatke", ToastType.Info);
                    return RedirectToAction("Index", new { bebaID = aktUpdate.BebaID });
                }
                catch (DataException)
                {
                    this.AddToastMessage("Pogreška", "Dogodila se pogreška.", ToastType.Error);
                    ModelState.AddModelError("", "Dogodila se greška prilikom spremanja podataka. Pokušajte ponovo.");
                }
            }
            ViewBag.bebaID = new SelectList(db.Bebas, "ID", "Ime", aktUpdate.BebaID);
            ViewBag.VrstaID = new SelectList(db.VrstaAkts, "ID", "Vrsta", aktUpdate.VrstaID);
            return View(aktUpdate);
        }


        // GET: Aktivnost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aktivnost aktivnost = db.Aktivnosts.Find(id);
            if (aktivnost == null)
            {
                return HttpNotFound();
            }
            return View(aktivnost);
        }

        // POST: Aktivnost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aktivnost aktivnost = db.Aktivnosts.Find(id);
            db.Aktivnosts.Remove(aktivnost);
            db.SaveChanges();
            this.AddToastMessage("Uspješno brisanje", "Uspješno ste obrisali podatke", ToastType.Warning);
            return RedirectToAction("Index", new { bebaID = aktivnost.BebaID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
