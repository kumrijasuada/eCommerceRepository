using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShisheVere.DBCONTEXT;
using ShisheVere.Models;
using ShisheVere.Security;

namespace ShisheVere.Controllers
{
    public class KategoriController : Controller
    {
        private StoreContext db = new StoreContext();

        [CostumAuthorize(Roles = "admin,user")]
        // GET: Kategori
        public ActionResult Index()
        {
            if (Session["roli"].ToString() == "admin")
                return View(db.Kategori.ToList());
            else return View("UserIndex", db.Kategori.ToList());
        }

        // GET: Kategori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [CostumAuthorize(Roles = "admin")]
        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        [CostumAuthorize(Roles = "admin")]
        // POST: Kategori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_kategori,Emertim,Status")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategori.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index","Admin");
        }

        [CostumAuthorize(Roles = "admin")]
        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [CostumAuthorize(Roles = "admin")]
        // POST: Kategori/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_kategori,Emertim,Status")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        [CostumAuthorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Kategori model = new Kategori();
            if (id > 0)
            {
                Kategori emp = db.Kategori.SingleOrDefault(x => x.Id_kategori == id);
                model.Id_kategori = emp.Id_kategori;
            }
            return PartialView("KategoriDelete", model);
        }

        [CostumAuthorize(Roles = "admin")]
        // POST: Kategori/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = db.Kategori.Find(id);
            db.Kategori.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
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
