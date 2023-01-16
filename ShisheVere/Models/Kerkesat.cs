using System;
using System.ComponentModel.DataAnnotations;

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