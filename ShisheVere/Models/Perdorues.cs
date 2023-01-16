using System.ComponentModel.DataAnnotations;

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
            public byte[] Salt { get; set; }
            [Required]
            public byte[] Password { get; set; }
            [Required]
            public string Roli { get; set; }
    }
}