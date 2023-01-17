using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShisheVere.Models
{
    public class Shishe
    {
        private Shishe shishe;

        public Shishe(Shishe shishe)
        {
            this.shishe = shishe;
        }

        public Shishe()
        {}

        [Key]
        public int Id_shishe { get; set; }
        
        public string Emertim { get; set; }
        
        public decimal Kapacitet { get; set; }
     
        public decimal Pesha { get; set; }
        
        public decimal Gjatesia { get; set; }
       
        public decimal Diametri { get; set; }

        public decimal Price { get; set; }
        public int Sasia { get; set; }
        public int Id_kategori { get; set; }

        public int Id_prodhues { get; set; }

        public string Status { get; set; }

        public virtual ICollection<Foto> Foto { get; set; }

        public virtual Kategori KATEGORI { get; set; }

        public virtual Prodhues Prodhues { get; set; }
    }


}