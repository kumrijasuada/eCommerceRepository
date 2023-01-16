using ShisheVere.DBCONTEXT;
using ShisheVere.Kategoriviewmodel;
using ShisheVere.Models;
using ShisheVere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Helpers;
using System.Data.Entity.Infrastructure;
using System.Web.UI.WebControls;
using System.IO;
using ShisheVere.Security;



namespace ShisheVere.Controllers
{
    public class AdminController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult Index()
        {
            var perdorues = db.Perdorues.Where(p => p.Roli == "user" || p.Roli == "prodhues").ToList();
            var shishe = db.Shishe.Include(s => s.KATEGORI).Include(s => s.Prodhues).ToList();
            var no = db.Notifications.Where(p => p.status == 0).ToList();
            AdminModel adm = new AdminModel();
            for (int i = 0; i < perdorues.Count; i++)
            {
                adm.perdorues.Add(perdorues[i]);
            }
            for (int i = 0; i < shishe.Count; i++)
            {
                adm.shishe.Add(shishe[i]);
            }
            for(int i = 0; i < no.Count; i++)
            {
                adm.notification.Add(no[i]);
            }
            ViewBag.No = db.Notifications.Where(n => n.status == 0).Count();
            ViewBag.Id = db.Perdorues.Where(p => p.Roli == "admin").Select(p => p.Id_perdorues).FirstOrDefault();
            return View(adm);
        }  


    }
}