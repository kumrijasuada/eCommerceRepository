using ShisheVere.DBCONTEXT;
using ShisheVere.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShisheVere.ViewModels
{
    
    public class AdminModel
    {
        StoreContext db = new StoreContext();
        public AdminModel()
        {
            shishe = new List<Shishe>();
            perdorues = new List<Perdorues>();
            notification = new List<Notification>();
            kategori = db.Kategori.ToList();
        }
        public List<Shishe> shishe { get; set; }
        public List<Perdorues> perdorues { get; set; }    
        public List<Notification> notification { get; set; }
        public List<Kategori> kategori { get; set; }
    }
}
