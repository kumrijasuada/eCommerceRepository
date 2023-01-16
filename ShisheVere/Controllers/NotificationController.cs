using ShisheVere.DBCONTEXT;
using ShisheVere.Models;
using System.Linq;
using System.Web.Mvc;

namespace ShisheVere.Controllers
{
    public class NotificationController : Controller
    {
        private StoreContext db = new StoreContext();
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Update(int id)
        {
            Notification no = db.Notifications.Where(p =>p.id == id).FirstOrDefault();
            no.status = 1;
            db.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}