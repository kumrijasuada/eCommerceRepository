using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShisheVere.DBCONTEXT;
using ShisheVere.Models;

namespace ShisheVere.Controllers
{
    public class FotoController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Foto
        public ActionResult Index()
        {
            var foto = db.Foto.Include(f => f.Shishe);
            return View(foto.ToList());
        }

        // GET: Foto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Foto.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            return View(foto);
        }

        // GET: Foto/Create
        public ActionResult Create()
        {
            ViewBag.Id_shishe = new SelectList(db.Shishe, "Id_shishe", "Emertim");
            return View();
        }

        // POST: Foto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_foto,Status,File,Id_shishe")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                db.Foto.Add(foto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_shishe = new SelectList(db.Shishe, "Id_shishe", "Emertim", foto.Id_shishe);
            return View(foto);
        }

        // GET: Foto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Foto.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_shishe = new SelectList(db.Shishe, "Id_shishe", "Emertim", foto.Id_shishe);
            return View(foto);
        }

        // POST: Foto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_foto,Status,File,Id_shishe")] Foto foto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_shishe = new SelectList(db.Shishe, "Id_shishe", "Emertim", foto.Id_shishe);
            return View(foto);
        }

        // GET: Foto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Foto foto = db.Foto.Find(id);
            if (foto == null)
            {
                return HttpNotFound();
            }
            return View(foto);
        }

        // POST: Foto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Foto foto = db.Foto.Find(id);
            db.Foto.Remove(foto);
            db.SaveChanges();
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
