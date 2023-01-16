using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShisheVere.Models
{
    public class Kerkesat
    {
        [Key]
        public string kerkesaId { get; set; }

        public int perdoruesId { get; set; }

        public DateTime date { get; set; }
    }
}