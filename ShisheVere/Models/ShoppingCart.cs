using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShisheVere.Models
{
    public class ShoppingCart
    {
            public int id { get; set; }
            public int Id_shishe { get; set; }
            public int Id_perdorues { get; set; }
            public string UserName { get; set; }
            public string Shishe { get; set; }
            public int Sasia { get; set; }       
            public decimal Price { get; set; }
            public string foto { get; set; }

    }
}
