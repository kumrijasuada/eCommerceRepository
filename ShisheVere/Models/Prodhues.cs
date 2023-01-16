using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShisheVere.Models
{
    public class Prodhues
    {
     
            [Key]
            public int Id_prodhues { get; set; }

            [Display(Name = "Prodhuesi")]
            [Required(ErrorMessage = "The product name cannot be blank")]
            public string Emertim { get; set; }

            [Required(ErrorMessage = "The adress cannot be blank")]
            public string Adrese { get; set; }

            [Required(ErrorMessage = "The email cannot be blank")]
            public string Email { get; set; }

            [Required(ErrorMessage = "The phone number cannot be blank")]
            public string Telefon { get; set; }
            [Required]
            public string Status { get; set; }

            public virtual ICollection<Shishe> Shishe { get; set; }
        
    }
}