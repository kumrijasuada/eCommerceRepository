namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclmfototoshopcart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCart", "foto_Id_foto", c => c.Int());
            CreateIndex("dbo.ShoppingCart", "foto_Id_foto");
            AddForeignKey("dbo.ShoppingCart", "foto_Id_foto", "dbo.Foto", "Id_foto");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCart", "foto_Id_foto", "dbo.Foto");
            DropIndex("dbo.ShoppingCart", new[] { "foto_Id_foto" });
            DropColumn("dbo.ShoppingCart", "foto_Id_foto");
        }
    }
}
