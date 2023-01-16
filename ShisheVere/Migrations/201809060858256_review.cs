namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class review : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCart", "Shishe_Id_shishe", c => c.Int());
            CreateIndex("dbo.ShoppingCart", "Shishe_Id_shishe");
            AddForeignKey("dbo.ShoppingCart", "Shishe_Id_shishe", "dbo.Shishe", "Id_shishe");
            DropColumn("dbo.ShoppingCart", "Shishe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "Shishe", c => c.String());
            DropForeignKey("dbo.ShoppingCart", "Shishe_Id_shishe", "dbo.Shishe");
            DropIndex("dbo.ShoppingCart", new[] { "Shishe_Id_shishe" });
            DropColumn("dbo.ShoppingCart", "Shishe_Id_shishe");
        }
    }
}
