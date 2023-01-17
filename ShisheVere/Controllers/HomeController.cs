using ShisheVere.DBCONTEXT;
using ShisheVere.Kategoriviewmodel;
using ShisheVere.Models;
using ShisheVere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Web.UI.WebControls;
using PagedList;

namespace AppShisheVere.Controllers
{

    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult Index(string search, int? page,int? gjatesi1,int? gjatesi2,int? diameter1,int? diameter2,string kategori)
        {
            int pageSize = 9;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<Shishe> shishe = null;
            shishe = db.Shishe.Include(s => s.KATEGORI).Include(s => s.Prodhues).Where(p => p.Status == "aktiv").OrderBy(p=>p.Id_shishe).ToPagedList(pageIndex, pageSize);
                     
            if (!string.IsNullOrEmpty(search))
            {
                if (search == "all") { }
                else
                shishe = shishe.Where(p => p.Emertim.Contains(search)).ToPagedList(pageIndex, pageSize);
            }

            if(gjatesi1 !=null || gjatesi2!=null)
            {
                if(gjatesi1 == null)
                    shishe = shishe.Where(p => p.Gjatesia > 0 && p.Gjatesia <=gjatesi2 ).ToPagedList(pageIndex, pageSize);
                else if(gjatesi2 == null)
                    shishe = shishe.Where(p => p.Gjatesia < 100 && p.Gjatesia >= gjatesi1).ToPagedList(pageIndex, pageSize);
                else
                    shishe = shishe.Where(p => p.Gjatesia >=gjatesi1 && p.Gjatesia <= gjatesi2).ToPagedList(pageIndex, pageSize);
            }

            if (diameter1 != null ||diameter2 != null)
            {
                if (diameter1 == null)
                    shishe = shishe.Where(p => p.Diametri > 0 && p.Diametri <= diameter2).ToPagedList(pageIndex, pageSize);
                else if (diameter2 == null)
                    shishe = shishe.Where(p => p.Diametri < 100 && p.Diametri >= diameter1).ToPagedList(pageIndex, pageSize);
                else
                    shishe = shishe.Where(p => p.Diametri >= diameter1 && p.Diametri <= diameter2).ToPagedList(pageIndex, pageSize);
            }

            if(!string.IsNullOrEmpty(kategori))
            {
                shishe = shishe.Where(p => p.KATEGORI.Emertim == kategori).ToPagedList(pageIndex, pageSize);
            }
            return View(shishe);
        }
        
        [HttpPost]
        public JsonResult JsonFoto()
        {
            List<Jsonfoto> foto = new List<Jsonfoto>();
            var res = db.Foto.ToList();
            foreach (var itm in res)
            {
                var row = new Jsonfoto();
                row.id_foto = itm.Id_foto;
                row.id_shishe = itm.Id_shishe;
                row.file = itm.File;
                row.emertim_shishe = itm.Shishe.Emertim;
                row.kategori = null;
                foto.Add(row);
            }

            return Json(foto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JsonSearch(string search)
        {
            List<Jsonfoto> foto = new List<Jsonfoto>();
            var res = db.Foto.Where(f => f.Shishe.Emertim.Contains(search) || f.Shishe.Prodhues.Emertim.Contains(search) || f.Shishe.KATEGORI.Emertim.Contains(search)).ToList();
            foreach (var itm in res)
            {
                var row = new Jsonfoto();

                row.id_foto = itm.Id_foto;
                row.id_shishe = itm.Id_shishe;
                row.file = itm.File;
                row.emertim_shishe = itm.Shishe.Emertim;
                row.kategori = itm.Shishe.KATEGORI.Emertim;
                foto.Add(row);
            }

            return Json(foto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Jsonkategori()
        {
            List<Kategorite> kategori = new List<Kategorite>();
            var res = db.Kategori.ToList();
            foreach (var itm in res)
            {
                var row = new Kategorite();
                row.Kategori = itm.Emertim;
                kategori.Add(row);
            }

            return Json(kategori, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}