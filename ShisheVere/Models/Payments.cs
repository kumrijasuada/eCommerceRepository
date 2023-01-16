using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShisheVere.Models
{
    public class Payments
    {
        public int Id { get; set; }
        public string TransaksionID { get; set; }
        public decimal Pagesa { get; set; }
        public string Statusi_Pagese { get; set; }
        public int ShisheID { get; set; }
        public DateTime Ora { get; set; }
        public string Valuta { get; set; }
        public int SasiaPorosi { get; set; }
        public string EmriShishe { get; set; }
        public string EmailBleres { get; set; }

    }
}