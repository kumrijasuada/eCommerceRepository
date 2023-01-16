using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShisheVere.Models
{
    public class Perdorues
    {
            [Key]
            public int Id_perdorues { get; set; }
            [Required]
            public string Emer { get; set; }
            [Required]
            public string Mbiemer { get; set; }
            [Required]
            public string Adrese { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Telefon { get; set; }
            [Required]
            public string Status { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string Roli { get; set; }
    }
}