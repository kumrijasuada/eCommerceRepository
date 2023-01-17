using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShisheVere.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        [DisplayName("Shishe")]
        public int Id_shishe { get; set; }
        public int Status { get; set; }
        public virtual Shishe Shishe { get; set; }
    }
}