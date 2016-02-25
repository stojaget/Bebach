using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BebachModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Bebach.Models;
using Microsoft.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Bebach.ViewModels;
using System.Web.Routing;
using Bebach.Extensions.Toastr;

namespace Bebach.Controllers
{
    public class BebaController : MessageControllerBase
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        
        private Entities db = new Entities();

        public BebaController()
        {
            db = new Entities();

        }


        [Authorize]
        // GET: Beba
        public ActionResult Index(int? id, int? aktID, int? pregledID)
        {
            var userId = User.Identity.GetUserId();
            var userManager = User.Identity.GetUserName().ToString();
            var roleManager = UserManager.GetRoles(userId);

            var viewModel = new BebaIndexData();
            viewModel.Bebas = db.Bebas
              .Include(i => i.Aktivnosts)
              .Include(i => i.Pregleds)
              .Include(i => i.Slikas)
              .OrderBy(i => i.Prezime);

            if (id != null)
            {
                ViewBag.BebaID = id.Value;
                viewModel.Aktivnosts = viewModel.Bebas.Where(
                    i => i.ID == id.Value).Single().Aktivnosts;
                viewModel.Pregleds = viewModel.Bebas.Where(i => i.ID == id.Value).Single().Pregleds;
                viewModel.Slikas = viewModel.Bebas.Where(i => i.ID == id.Value).Single().Slikas;
               // viewModel.Slikas = viewModel.BebaSlikas.Where(i => i.BebaID == id.Value).Single().Slika;
            }
            //if (aktID != null)
            //{
            // return   RedirectToAction("Index", new RouteValueDictionary(new { controller = Aktivnosts, action = "Index", id = aktID }));
            //}
            //if (pregledID != null)
            //{
            //    return RedirectToAction("Index", new RouteValueDictionary(new { controller = Pregleds, action = "Index", id = pregledID }));
            //}

            if (User.IsInRole("Admin"))
            {
                return View(viewModel);
            }

            else
            {
                return View(viewModel.Bebas.Where(x => x.Unio.Trim().ToString() == userManager.Trim().ToString()));
            }
        }

        // GET: Beba/Details/5
       [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beba beba = db.Bebas.Find(id);
            if (beba == null)
            {
                return HttpNotFound();
            }
            return View(beba);
        }

        // GET: Beba/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Ime,Prezime,Dat_rod,OIB,Adresa,Majka,Otac,Dat_kreiranja,Dat_azu,Unio,Aktivan,Racun")] Beba beba)
        {
            var userManager = User.Identity.GetUserName().ToString();
            try
            {
                if (ModelState.IsValid)
                {
                    beba.Unio = userManager.ToString();
                    db.Bebas.Add(beba);
                    db.SaveChanges();
                    this.AddToastMessage("Uspješan unos", "Uspješno ste unijeli nove podatke", ToastType.Success);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                this.AddToastMessage("Pogreška", "Dogodila se pogreška.", ToastType.Error);
                ModelState.AddModelError("", "Dogodila se greška prilikom spremanja podataka. Pokušajte ponovo.");
            }

            return View(beba);
        }

        // GET: Beba/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beba beba = db.Bebas.Find(id);
            if (beba == null)
            {
                return HttpNotFound();
            }
            return View(beba);
        }

        // POST: Beba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditBeba(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userManager = User.Identity.GetUserName().ToString();
            var bebaUpdate = db.Bebas.Find(id);
            if (TryUpdateModel(bebaUpdate, "", new string[] { "Ime", "Prezime", "Dat_rod", "OIB", "Adresa", "Majka", "Otac", "Aktivan", "Racun" }))
            {
                try
                {
                    bebaUpdate.Dat_azu = DateTime.Now;
                    bebaUpdate.Unio = userManager;
                    db.SaveChanges();
                    this.AddToastMessage("Uspješan unos", "Uspješno ste promijenili podatke", ToastType.Info);
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {

                    ModelState.AddModelError("", "Dogodila se greška prilikom spremanja podataka. Pokušajte ponovo.");
                }
            }

            return View(bebaUpdate);
        }

        // GET: Beba/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Brisanje nije uspjelo. Provjerite da beba ima povezane aktivnosti i njih obrišite prvo.";


            }

            Beba beba = db.Bebas.Find(id);
            if (beba == null)
            {
                return HttpNotFound();
            }
            return View(beba);
        }

        // POST: Beba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Beba beba = db.Bebas.Find(id);
                db.Bebas.Remove(beba);
                this.AddToastMessage("Uspješno brisanje", "Uspješno ste obrisali podatke", ToastType.Warning);
                db.SaveChanges();
            }
            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("Index");
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
