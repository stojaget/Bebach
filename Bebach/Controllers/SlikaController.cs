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
using System.Drawing;
using Bebach.Extensions.Toastr;

namespace Bebach.Controllers
{
    public class SlikaController : MessageControllerBase
    {
    
        private Entities db = new Entities();
        
        
        // GET: Slika
        public ActionResult Index(int page = 1, int pageSize = 20, string filter = null, int bebaID = 0)
        {
            var records = new PagedList<Slika>();
            var bebe = db.Bebas.Where(i => i.ID == bebaID).Single().Slikas;
            ViewBag.filter = filter;
            records.Content = db.Slikas
                .Where(x => filter == null || (x.Opis.Contains(filter)) && x.BebaID == bebaID)
                .OrderByDescending(x => x.ID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            //count
            records.TotalRecords = db.Slikas.Where(x => filter == null || (x.Opis.Contains(filter))).Count();
            records.CurrentPage = page;
            records.PageSize = pageSize;
            return View(records);
        }

        // GET: Slika/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slika slika = db.Slikas.Find(id);
            if (slika == null)
            {
                return HttpNotFound();
            }
            return View(slika);
        }

        // GET: Slika/Create
        public ActionResult Create()
        {
            var photo = new Slika();
            return View(photo);
        }

        // POST: Slika/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Opis,Putanja_Thumb,Putanja_Slika,Dat_kreiranja,BebaID")] Slika slika, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)

                return View(slika);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(slika);
            }

            var model = new Slika();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Opis = slika.Opis;
                var fileName = Guid.NewGuid().ToString();
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();
                using (var img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    model.Putanja_Thumb = String.Format("/GalleryImages/thumbs/{0}{1}", fileName, extension);
                    model.Putanja_Slika = String.Format("/GalleryImages/{0}{1}", fileName, extension);

                    // Save thumbnail size image, 100 x 100
                    Extensions.SlikeHelper.SaveToFolder(img, fileName, extension, new Size(100, 100), model.Putanja_Thumb);

                    // Save large size image, 800 x 800
                    Extensions.SlikeHelper.SaveToFolder(img, fileName, extension, new Size(600, 600), model.Putanja_Slika);
                }
                model.Dat_kreiranja = DateTime.Now;

                db.Slikas.Add(model);
                db.SaveChanges();
                this.AddToastMessage("Uspješan unos", "Uspješno ste unijeli novu sliku", ToastType.Success);
            }


            return RedirectToAction("Index");
        }


        // GET: Slika/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slika slika = db.Slikas.Find(id);
            if (slika == null)
            {
                return HttpNotFound();
            }
            return View(slika);
        }

        // POST: Slika/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Opis,Putanja_Thumb,Putanja_Slika,Dat_kreiranja")] Slika slika)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slika).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slika);
        }

        // GET: Slika/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slika slika = db.Slikas.Find(id);
            if (slika == null)
            {
                return HttpNotFound();
            }
            return View(slika);
        }

        // POST: Slika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slika slika = db.Slikas.Find(id);
            db.Slikas.Remove(slika);
            db.SaveChanges();
            this.AddToastMessage("Uspješno brisanje", "Uspješno ste obrisali podatke", ToastType.Warning);
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
