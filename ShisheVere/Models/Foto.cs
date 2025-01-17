﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShisheVere.Models
{
    public class Foto
    {
        [Key]
        public int Id_foto { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string File { get; set; }

        [DisplayName("Shishe")]
        public int Id_shishe { get; set; }

        public virtual Shishe Shishe { get; set; }
    }
}