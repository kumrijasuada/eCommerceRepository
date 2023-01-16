namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringfoto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCart", "foto_Id_foto", "dbo.Foto");
            DropIndex("dbo.ShoppingCart", new[] { "foto_Id_foto" });
            AddColumn("dbo.ShoppingCart", "foto", c => c.String());
            DropColumn("dbo.ShoppingCart", "foto_Id_foto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "foto_Id_foto", c => c.Int());
            DropColumn("dbo.ShoppingCart", "foto");
            CreateIndex("dbo.ShoppingCart", "foto_Id_foto");
            AddForeignKey("dbo.ShoppingCart", "foto_Id_foto", "dbo.Foto", "Id_foto");
        }
    }
}
