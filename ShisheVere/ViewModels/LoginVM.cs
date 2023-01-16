using ShisheVere.DBCONTEXT;
using ShisheVere.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShisheVere.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string ReturnURL { get; set; }

        public bool isRemember { get; set; }

        public string Roli { get; set; }

        public Perdorues Gjej(string username)
        {
            using (StoreContext db = new StoreContext())
                return db.Perdorues.Where(p => p.Username.Equals(username)).FirstOrDefault();
        }
    }

    public class RegistrationView
    {
        [Required(ErrorMessage = "Emer required")]
        [Display(Name = "Emer")]
        public string Emer { get; set; }

        [Required(ErrorMessage = "Mbiemer required")]
        [Display(Name = "Mbiemer")]
        public string Mbiemer { get; set; }

        [Required(ErrorMessage = "Email required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number required")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "Choose Role")]
        public string Roli { get; set; }
        [Required(ErrorMessage = "Adresa required")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Username required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Error : Confirm password does not match with password")]
        public string ConfirmPassword { get; set; }
    }
}