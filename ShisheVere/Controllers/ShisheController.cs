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
using System.Web.Helpers;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using System.IO;
using ShisheVere.Security;
using ShisheVere.ViewModels;

namespace AppShisheVere.Controllers
{
    public class ShisheController : Controller
    {
        private StoreContext db = new StoreContext();

        [CostumAuthorize(Roles = "admin,user")]
        // GET: Shishe
        public ActionResult Index(string search)
        {
            var shishe = db.Shishe.Include(s => s.KATEGORI).Include(s => s.Prodhues);

            if (!string.IsNullOrEmpty(search))
            {
                shishe = shishe.Where(p => p.KATEGORI.Emertim.Equals(search) || p.Emertim.Contains(search) || p.Prodhues.Emertim.Contains(search));
                ViewBag.Search = search;
            }

            IEnumerable<SelectListItem> x = shishe.OrderBy(p => p.Gjatesia).Select(c => new SelectListItem { Value = c.Gjatesia.ToString(), Text = c.Gjatesia.ToString() }).Distinct();
            ViewBag.Gjatesi = x;
            IEnumerable<SelectListItem> y = shishe.OrderBy(p => p.Diametri).Select(c => new SelectListItem { Value = c.Diametri.ToString(), Text = c.Diametri.ToString() }).Distinct();
            ViewBag.Diametri = y;
            if (Session["roli"].ToString() == "admin")
                return View(shishe.ToList());
            else return View("UserIndex", shishe.ToList());
        }


        [CostumAuthorize(Roles = "admin,user")]
        // GET: Shishe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shishe shishe = db.Shishe.Find(id);
            if (shishe == null)
            {
                return HttpNotFound();
            }
            if (Session["roli"].ToString() == "admin")
                return View(shishe);
            else return View("UserDetails", shishe);
        }

        // GET: Shishe/Create
        [CostumAuthorize(Roles = "admin,prodhues")]
        public ActionResult Create()
        {
            ViewBag.id_kategori = new SelectList(db.Kategori, "Id_kategori", "Emertim");
            ViewBag.id_prodhues = new SelectList(db.Prodhues, "Id_prodhues", "Emertim");
            return View();
        }

        [CostumAuthorize(Roles = "admin,prodhues")]
        // POST: Shishe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form, HttpPostedFileBase file)
        {
            Shishe shishe = new Shishe();
            shishe.Emertim = form["Emertim"].ToString();
            shishe.Kapacitet = Convert.ToDecimal(form["Kapacitet"]);
            shishe.Pesha = Convert.ToDecimal(form["Pesha"]);
            shishe.Gjatesia = Convert.ToDecimal(form["Gjatesia"]);
            shishe.Diametri = Convert.ToDecimal(form["Diametri"]);
            shishe.Price = Convert.ToDecimal(form["Price"]);
            shishe.id_kategori = Convert.ToInt32(form["id_kategori"]);
            shishe.id_prodhues = Convert.ToInt32(form["id_prodhues"]);
            shishe.status = "pritje";
            if (ModelState.IsValid)
            {
                db.Shishe.Add(shishe);
                db.SaveChanges();
                if (file != null)
                {
                    Foto foto = new Foto();
                    var allowedExtensions = new[] { ".jpg", ".png", ".jpg", "jpeg" };
                    foto.Status = "aktiv";
                    foto.File = "/Images/" + file.FileName;
                    foto.Id_shishe = shishe.Id_shishe;
                    var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + ext; //appending the name with id  
                                                    // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath("/Images/"), myfile);
                        foto.File = "/Images/" + file.FileName;
                        db.Foto.Add(foto);
                        db.SaveChanges();
                        file.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.id_kategori = new SelectList(db.Kategori, "Id_kategori", "Emertim", shishe.id_kategori);
            ViewBag.id_prodhues = new SelectList(db.Prodhues, "Id_prodhues", "Emertim", shishe.id_prodhues);
            return View(shishe);
        }
        [CostumAuthorize(Roles = "admin")]
        // GET: Shishe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shishe shishe = db.Shishe.Find(id);
            if (shishe == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_kategori = new SelectList(db.Kategori, "Id_kategori", "Emertim", shishe.id_kategori);
            ViewBag.id_prodhues = new SelectList(db.Prodhues, "Id_prodhues", "Emertim", shishe.id_prodhues);
            return View(shishe);
        }
        [CostumAuthorize(Roles = "admin")]
        // POST: Shishe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Shishe shishe, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shishe).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (file != null)
            {
                var f = db.Foto.Where(p=>p.Id_shishe==shishe.Id_shishe).FirstOrDefault();
                Foto foto = new Foto();
                var allowedExtensions = new[] { ".jpg", ".png", ".jpg", "jpeg" };
                foto.Status = "aktiv";
                foto.File = "/Images/" + file.FileName;
                foto.Id_shishe = shishe.Id_shishe;
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + ext; //appending the name with id  
                                                // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("/Images/"), myfile);
                    foto.File = "/Images/" + file.FileName;
                    f.Id_shishe = foto.Id_shishe;
                    f.File = foto.File;
                    f.Status = "aktiv";
                    db.SaveChanges();
                    file.SaveAs(path);
                    TempData["edit"] = "Modifikimi u krye me sukses !";
                }
                else
                {
                    ViewBag.message = "Please choose only Image file";
                }
            }
            ViewBag.id_kategori = new SelectList(db.Kategori, "Id_kategori", "Emertim", shishe.id_kategori);
            ViewBag.id_prodhues = new SelectList(db.Prodhues, "Id_prodhues", "Emertim", shishe.id_prodhues);           
            return View();
        }


        
        public ActionResult Delete(int id)
        {
            Shishe model = new Shishe();
            if (id > 0)
            {
                Shishe emp = db.Shishe.SingleOrDefault(x => x.Id_shishe == id);
                model.Id_shishe = emp.Id_shishe;
            }
            return PartialView("DeleteShishe", model);
        }

      
        // POST: Kategori/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            Shishe sh = db.Shishe.Find(id);
            db.Shishe.Remove(sh);
            db.SaveChanges();
            return RedirectToAction("Index", "Prodhues");
        }



        [HttpPost]
      public JsonResult Update (ShisheModel shishe)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Shishe sh = db.Shishe.Where(p => p.Id_shishe == shishe.Id).FirstOrDefault();
            sh.status = shishe.Status;
            db.SaveChanges();           
            return Json(sh, JsonRequestBehavior.AllowGet);
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
