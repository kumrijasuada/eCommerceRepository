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
using System.IO;
using PagedList;

namespace ShisheVere.Controllers
{
    public class ProdhuesController : Controller
    {

        private StoreContext db = new StoreContext();

        [CostumAuthorize(Roles = "prodhues")]
        // GET: Prodhues
        public ActionResult Index(string search)
        {
            ViewBag.Kategori = db.Kategori.Select(d => new SelectListItem { Text = d.Emertim, Value = d.Id_kategori.ToString() });
            var UserName = Session["UserName"].ToString();
            List<Shishe> shishe = db.Shishe.Include(s => s.Prodhues).Where(p => p.Prodhues.Emertim.Equals(UserName)).OrderBy(p => p.Id_shishe).ToList();
            return View(shishe);
        }

        [CostumAuthorize(Roles = "prodhues")]
        public ActionResult Transaksione(string search)
        {
            //ViewBag.Kategori = db.Kategori.Select(d => new SelectListItem { Text = d.Emertim, Value = d.Id_kategori.ToString() });
            //var UserName = Session["UserName"].ToString();
            //List<Shishe> shishe = db.Shishe.ToList();
            //List<Payments> payments = db.Payments.ToList();
            //List<Prodhues> prodhues = db.Prodhues.ToList();
            //var transaksioni = from p in payments
            //                   join sh in shishe on p.ShisheID equals sh.Id_shishe into table1
            //                   from sh in table1.ToList()
            //                   join i in prodhues on sh.id_prodhues equals i.Id_prodhues into table2
            //                   from i in table2.ToList().Where(i => i.Emertim.Equals(UserName))
            //                   select new ViewModel
            //                   {
            //                       payments = p,
            //                       shishe = sh,
            //                       prodhues = i
            //                   };
            return View();
        }


        [CostumAuthorize(Roles = "prodhues")]
        public JsonResult Transaksione1(string search)
        {
            ViewBag.Kategori = db.Kategori.Select(d => new SelectListItem { Text = d.Emertim, Value = d.Id_kategori.ToString() });
            var UserName = Session["UserName"].ToString();
            var idProdhues = db.Prodhues.Where(s => s.Emertim.Equals(UserName)).ToList();
            
            List<Payments> payments = db.Payments.ToList();
            var data1 = payments.ToList();
            
            return Json(new { data = data1}, JsonRequestBehavior.AllowGet);
        }

        [CostumAuthorize(Roles = "prodhues")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateShishe(FormCollection form, HttpPostedFileBase file)
        {
            Shishe shishe = new Shishe();
            shishe.Emertim = form["Emertim"].ToString();
            shishe.Kapacitet = Convert.ToDecimal(form["Kapacitet"]);
            shishe.Pesha = Convert.ToDecimal(form["Pesha"]);
            shishe.Gjatesia = Convert.ToDecimal(form["Gjatesia"]);
            shishe.Sasia= Convert.ToInt32(form["Sasia"]);
            shishe.Diametri = Convert.ToDecimal(form["Diametri"]);
            shishe.id_kategori = Convert.ToInt32(form["Kategori"]);
            shishe.Price = Convert.ToDecimal(form["Price"]);
            string str = Session["UserName"].ToString();
            int p1 = db.Prodhues.Where(p => p.Emertim == str).Select(p => p.Id_prodhues).FirstOrDefault();
            shishe.id_prodhues = p1;
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
                var UserName = Session["UserName"].ToString();
                string prodhues_name = db.Perdorues.Where(x => x.Username == UserName).Select(x => x.Emer).FirstOrDefault()+" "+ db.Perdorues.Where(x => x.Username == UserName).Select(x => x.Mbiemer).FirstOrDefault();
                Notification no = new Notification();
                no.name = prodhues_name;
                no.id_shishe = shishe.Id_shishe;
                no.action = "shtoi nje shishe te re";
                no.status = 0;
                db.Notifications.Add(no);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shishe);
        }

        public ActionResult Afisho(int id)
        {
            Session["id"] = id;
            Shishe model = new Shishe();
            if (id > 0)
            {
                Shishe emp = db.Shishe.SingleOrDefault(x => x.Id_shishe == id);
                model.Emertim = emp.Emertim;
                model.Diametri = emp.Diametri;
                model.Kapacitet = emp.Kapacitet;
                model.Pesha = emp.Pesha;
                model.Gjatesia = emp.Gjatesia;
                model.status = "pritje";
            }
            return PartialView("_View", model);
        }


        public ActionResult Afisho1(int id)
        {
            Shishe model = new Shishe();
            if (id > 0)
            {
                Shishe emp = db.Shishe.SingleOrDefault(x => x.Id_shishe == id);
                model.Emertim = emp.Emertim;
                model.Diametri = emp.Diametri;
                model.Kapacitet = emp.Kapacitet;
                model.Pesha = emp.Pesha;
                model.Gjatesia = emp.Gjatesia;
                model.Price = emp.Price;
                model.status = "pritje";
            }
            return PartialView("View1", model);
        }


        [CostumAuthorize(Roles = "prodhues")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditShishe(Shishe shishe, HttpPostedFileBase file)
        {
            int id = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                var sh = db.Shishe.Where(p => p.Id_shishe == id).FirstOrDefault();
                sh.Id_shishe = id;
                sh.Emertim = shishe.Emertim;
                sh.Diametri = shishe.Diametri;
                sh.Gjatesia = shishe.Gjatesia;
                sh.Kapacitet = shishe.Kapacitet;
                sh.Pesha = shishe.Pesha;
                db.SaveChanges();
            }
            if (file != null)
            {
                var f = db.Foto.Where(p => p.Id_shishe == id).FirstOrDefault();
                Foto foto = new Foto();
                var allowedExtensions = new[] { ".jpg", ".png", ".jpg", "jpeg" };
                foto.Status = "aktiv";
                foto.File = "/Images/" + file.FileName;
                foto.Id_shishe = id;
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                if (allowedExtensions.Contains(ext)) //check what type of extension  
                {
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + ext; //appending the name with id  
                                                // store the file inside ~/project folder(Img)  
                    var path = Path.Combine(Server.MapPath("/Images/"), myfile);
                    foto.File = "/Images/" + file.FileName;
                    f.Id_shishe = id;
                    f.File = foto.File;
                    f.Status = "aktiv";
                    db.SaveChanges();
                    file.SaveAs(path);
                }
                else
                {

                }
            }
            return RedirectToAction("Index");

        }



        // GET: Prodhues/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodhues prodhues = db.Prodhues.Find(id);
            if (prodhues == null)
            {
                return HttpNotFound();
            }
            return View(prodhues);
        }

        [CostumAuthorize(Roles = "admin")]
        // GET: Prodhues/Create
        public ActionResult Create()
        {
            return View();
        }

        [CostumAuthorize(Roles = "admin")]
        // POST: Prodhues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_prodhues,Emertim,Adrese,Email,Telefon,Status")] Prodhues prodhues)
        {
            if (ModelState.IsValid)
            {
                db.Prodhues.Add(prodhues);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodhues);
        }

        [CostumAuthorize(Roles = "admin")]
        // GET: Prodhues/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodhues prodhues = db.Prodhues.Find(id);
            if (prodhues == null)
            {
                return HttpNotFound();
            }
            return View(prodhues);
        }

        [CostumAuthorize(Roles = "admin")]
        // POST: Prodhues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_prodhues,Emertim,Adrese,Email,Telefon,Status")] Prodhues prodhues)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodhues).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodhues);
        }

        [CostumAuthorize(Roles = "admin")]

        // GET: Prodhues/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodhues prodhues = db.Prodhues.Find(id);
            if (prodhues == null)
            {
                return HttpNotFound();
            }
            return View(prodhues);
        }

        [CostumAuthorize(Roles = "admin")]

        // POST: Prodhues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodhues prodhues = db.Prodhues.Find(id);
            db.Prodhues.Remove(prodhues);
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
