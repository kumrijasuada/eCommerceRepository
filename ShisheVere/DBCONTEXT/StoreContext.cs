using ShisheVere.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ShisheVere.DBCONTEXT
{
    public class StoreContext:DbContext
    {
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Perdorues> Perdorues { get; set; }
        public DbSet<Prodhues> Prodhues { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Shishe> Shishe { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Kerkesat> Kerkesat { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Payments> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}