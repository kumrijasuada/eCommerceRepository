namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShisheTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            DropIndex("dbo.Shishe", new[] { "id_prodhues" });
            CreateIndex("dbo.Shishe", "Id_kategori");
            CreateIndex("dbo.Shishe", "Id_prodhues");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Shishe", new[] { "Id_prodhues" });
            DropIndex("dbo.Shishe", new[] { "Id_kategori" });
            CreateIndex("dbo.Shishe", "id_prodhues");
            CreateIndex("dbo.Shishe", "id_kategori");
        }
    }
}
