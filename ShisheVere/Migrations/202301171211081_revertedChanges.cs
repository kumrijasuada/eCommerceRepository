namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revertedChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Kerkesat", "Id_perdorues", "dbo.Perdorues");
            DropForeignKey("dbo.Notification", "Id_shishe", "dbo.Shishe");
            DropForeignKey("dbo.Orders", "Id_perdorues", "dbo.Perdorues");
            DropForeignKey("dbo.Orders", "Id_shishe", "dbo.Shishe");
            DropForeignKey("dbo.Payments", "Id_shishe", "dbo.Shishe");
            DropForeignKey("dbo.ShoppingCart", "Id_perdorues", "dbo.Perdorues");
            DropForeignKey("dbo.ShoppingCart", "Id_shishe", "dbo.Shishe");
            DropIndex("dbo.Shishe", new[] { "Id_kategori" });
            DropIndex("dbo.Shishe", new[] { "Id_prodhues" });
            DropIndex("dbo.Kerkesat", new[] { "Id_perdorues" });
            DropIndex("dbo.Notification", new[] { "Id_shishe" });
            DropIndex("dbo.Orders", new[] { "Id_shishe" });
            DropIndex("dbo.Orders", new[] { "Id_perdorues" });
            DropIndex("dbo.Payments", new[] { "Id_shishe" });
            DropIndex("dbo.ShoppingCart", new[] { "Id_shishe" });
            DropIndex("dbo.ShoppingCart", new[] { "Id_perdorues" });
            AddColumn("dbo.Kerkesat", "perdoruesId", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "ShisheID", c => c.Int(nullable: false));
            CreateIndex("dbo.Shishe", "id_kategori");
            CreateIndex("dbo.Shishe", "id_prodhues");
            DropColumn("dbo.Kerkesat", "Id_perdorues");
            DropColumn("dbo.Payments", "Id_shishe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "Id_shishe", c => c.Int(nullable: false));
            AddColumn("dbo.Kerkesat", "Id_perdorues", c => c.Int(nullable: false));
            DropIndex("dbo.Shishe", new[] { "id_prodhues" });
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            DropColumn("dbo.Payments", "ShisheID");
            DropColumn("dbo.Kerkesat", "perdoruesId");
            CreateIndex("dbo.ShoppingCart", "Id_perdorues");
            CreateIndex("dbo.ShoppingCart", "Id_shishe");
            CreateIndex("dbo.Payments", "Id_shishe");
            CreateIndex("dbo.Orders", "Id_perdorues");
            CreateIndex("dbo.Orders", "Id_shishe");
            CreateIndex("dbo.Notification", "Id_shishe");
            CreateIndex("dbo.Kerkesat", "Id_perdorues");
            CreateIndex("dbo.Shishe", "Id_prodhues");
            CreateIndex("dbo.Shishe", "Id_kategori");
            AddForeignKey("dbo.ShoppingCart", "Id_shishe", "dbo.Shishe", "Id_shishe", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingCart", "Id_perdorues", "dbo.Perdorues", "Id_perdorues", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "Id_shishe", "dbo.Shishe", "Id_shishe", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Id_shishe", "dbo.Shishe", "Id_shishe", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Id_perdorues", "dbo.Perdorues", "Id_perdorues", cascadeDelete: true);
            AddForeignKey("dbo.Notification", "Id_shishe", "dbo.Shishe", "Id_shishe", cascadeDelete: true);
            AddForeignKey("dbo.Kerkesat", "Id_perdorues", "dbo.Perdorues", "Id_perdorues", cascadeDelete: true);
        }
    }
}
