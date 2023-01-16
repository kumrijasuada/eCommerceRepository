using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShisheVere.Models
{
    public class Kategori
    {
        [Key]
        public int Id_kategori { get; set; }
        [Required]
        [Display(Name = "Kategoria")]
        public string Emertim { get; set; }
        [Required]
        public string Status { get; set; }
        public virtual ICollection<Shishe> Shishe { get; set; }
    }
}