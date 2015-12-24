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
    public class PregledController :  MessageControllerBase
    {
        private Entities db = new Entities();

        // GET: Pregled
        public ActionResult Index(int? bebaID, string sortOrder)
        {
            if (bebaID != null)
            {
                ViewBag.BebaID = bebaID.Value;
                var pregleds = db.Pregleds.Include(a => a.Beba).Where(
                    i => i.BebaID == bebaID.Value).ToList();
                ViewBag.DateSortParm = sortOrder == "Datum" ? "date_desc" : "Datum";
                var sortirane = from s in pregleds
                                select s;
                switch (sortOrder)
                {
                   
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
                ViewBag.BebaID = bebaID.Value;
                var pregleds = db.Pregleds.Include(a => a.Beba).Where(
                    i => i.BebaID == bebaID.Value).ToList();
                // napraviti sortiranje po ID, Datum poljima na aktivnosts


                var searchRes = pregleds.Where(p => p.Datum >= datOd && p.Datum <= datDo);

                return View("Index", searchRes.ToList());

            }
            else
            {
                return new EmptyResult();
            }




        }
        // GET: Pregled/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregled pregled = db.Pregleds.Find(id);
            if (pregled == null)
            {
                return HttpNotFound();
            }
            return View(pregled);
        }

        // GET: Pregled/Create
        public ActionResult Create(int bebaID = 0)
        {
            ViewBag.BebaDropID = new SelectList(db.Bebas, "ID", "Ime");
            ViewBag.bebaID = bebaID;
            Pregled initPregled = new Pregled();
            initPregled.Datum = DateTime.Now;
           // initPregled.BebaID = bebaID;
            initPregled.Bolesti = "";
            initPregled.Opis = "";
            initPregled.OpsegGlave = 0.00m;
            initPregled.Tezina = 0.00m;
            initPregled.Visina = 0.00m;
            return View(initPregled);
        }

        // POST: Pregled/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Datum,Opis,Visina,Tezina,Bolesti,OpsegGlave,BebaID")] Pregled pregled)
        {
            if (ModelState.IsValid)
            {
                if (pregled.Datum == null)
                {
                    pregled.Datum = DateTime.Now;
                }
                db.Pregleds.Add(pregled);
                db.SaveChanges();
                this.AddToastMessage("Uspješan unos", "Uspješno ste unijeli novi pregled", ToastType.Success);
                return RedirectToAction("Index", new { bebaID = pregled.BebaID });
            }

            ViewBag.BebaDropID = new SelectList(db.Bebas, "ID", "Ime", pregled.BebaID);
            return View(pregled);
        }

        // GET: Pregled/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregled pregled = db.Pregleds.Find(id);
            if (pregled == null)
            {
                return HttpNotFound();
            }
            ViewBag.BebaID = new SelectList(db.Bebas, "ID", "Ime", pregled.BebaID);
            return View(pregled);
        }

        // POST: Pregled/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPregled(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var preUpdate = db.Pregleds.Find(id);
            if (TryUpdateModel(preUpdate, "", new string[] { "Datum", "Opis", "Visina", "Tezina", "Bolesti", "OpsegGlave", "BebaID" }))
            {
                try
                {
                    if (preUpdate.Datum == null)
                    {
                        preUpdate.Datum = DateTime.Now;
                    }

                    db.SaveChanges();
                    this.AddToastMessage("Uspješan unos", "Uspješno ste promijenili podatke", ToastType.Info);
                    return RedirectToAction("Index", new { bebaID = preUpdate.BebaID });
                }
                catch (DataException)
                {
                    this.AddToastMessage("Pogreška", "Dogodila se pogreška.", ToastType.Error);
                    ModelState.AddModelError("", "Dogodila se greška prilikom spremanja podataka. Pokušajte ponovo.");
                }
            }
           
            ViewBag.BebaID = new SelectList(db.Bebas, "ID", "Ime", preUpdate.BebaID);
            return View(preUpdate);
        }

        // GET: Pregled/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregled pregled = db.Pregleds.Find(id);
            if (pregled == null)
            {
                return HttpNotFound();
            }
            return View(pregled);
        }

        // POST: Pregled/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregled pregled = db.Pregleds.Find(id);
            db.Pregleds.Remove(pregled);
            db.SaveChanges();
            this.AddToastMessage("Uspješno brisanje", "Uspješno ste obrisali podatke", ToastType.Warning);
            return RedirectToAction("Index", new { bebaID = pregled.BebaID });
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
