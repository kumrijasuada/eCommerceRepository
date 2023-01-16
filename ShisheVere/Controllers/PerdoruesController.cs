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
using ShisheVere.Security;

namespace ShisheVere.Controllers
{
    public class PerdoruesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Perdorues
        public ActionResult Index()
        {

            return View(db.Perdorues.Where(p=>p.Roli=="user" || p.Roli=="prodhues").ToList());
        }

        // GET: Perdorues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perdorues perdorues = db.Perdorues.Find(id);
            if (perdorues == null)
            {
                return HttpNotFound();
            }
            return View(perdorues);
        }

        public ActionResult Edit(int id)
        {
            Session["id"] = id;
            Perdorues model = new Perdorues();
            if (id > 0)
            {
                Perdorues emp = db.Perdorues.SingleOrDefault(x => x.Id_perdorues == id);
                model.Emer = emp.Emer;
                model.Mbiemer = emp.Mbiemer;
                model.Adrese = emp.Adrese;
                model.Email = emp.Email;
                model.Password = emp.Password;
                model.Roli = emp.Roli;
                model.Status = emp.Status;
                model.Telefon = emp.Telefon;
                model.Username = emp.Username;
                model.Password = emp.Password;
            }
            return PartialView("PerdoruesEdit", model);
        }


        // POST: Perdorues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Perdorues emp)
        {
            int id = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                var model = db.Perdorues.Where(p => p.Id_perdorues == id).FirstOrDefault();
                model.Emer = emp.Emer;
                model.Mbiemer = emp.Mbiemer;
                model.Adrese = emp.Adrese;
                model.Email = emp.Email;
                model.Password = emp.Password;
                model.Roli = emp.Roli;
                model.Status = emp.Status;
                model.Telefon = emp.Telefon;
                model.Username = emp.Username;
                model.Password = emp.Password;
                db.SaveChanges();
            }
            return RedirectToAction("Index","Admin");
        }

        [Authorize]
        public ActionResult EditProfile()
        {
                string user = Session["UserName"].ToString();
                int id = db.Perdorues.Where(x => x.Username == user).Select(x => x.Id_perdorues).FirstOrDefault();
                Session["id1"] = id;
                Perdorues model = new Perdorues();
                if (id > 0)
                {
                    Perdorues emp = db.Perdorues.SingleOrDefault(x => x.Id_perdorues == id);
                    model.Emer = emp.Emer;
                    model.Mbiemer = emp.Mbiemer;
                    model.Adrese = emp.Adrese;
                    model.Email = emp.Email;
                    model.Password = emp.Password;
                    model.Roli = emp.Roli;
                    model.Status = emp.Status;
                    model.Telefon = emp.Telefon;
                    model.Username = emp.Username;
                    model.Password = emp.Password;
                }

                return View(model);
        }

       
        // POST: Perdorues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditProfile(Perdorues emp)
        {
            int id = Convert.ToInt32(Session["id1"]);
            Perdorues model = new Perdorues();
            if (ModelState.IsValid)
            {
                model = db.Perdorues.Where(p => p.Id_perdorues == id).FirstOrDefault();
                model.Emer = emp.Emer;
                model.Mbiemer = emp.Mbiemer;
                model.Adrese = emp.Adrese;
                model.Email = emp.Email;
                model.Password = emp.Password;
                model.Roli = model.Roli;
                model.Status = model.Status;
                model.Telefon = emp.Telefon;
                model.Username = emp.Username;
                model.Password = emp.Password;
                db.SaveChanges();
            }
            return RedirectToAction("EditProfile", "Perdorues");
        }

        public ActionResult Delete(int id)
        {
            Session["delete"] = id;
            Perdorues model = new Perdorues();
            if (id > 0)
            {
                Perdorues emp = db.Perdorues.SingleOrDefault(x => x.Id_perdorues == id);
                model.Id_perdorues = emp.Id_perdorues;             
            }
            return PartialView("PerdoruesDelete", model);
        }

        // POST: Perdorues/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perdorues perdorues = db.Perdorues.Where(p=>p.Id_perdorues==id).FirstOrDefault();
            db.Perdorues.Remove(perdorues);
            db.SaveChanges();
            return RedirectToAction("Index","Admin");
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
