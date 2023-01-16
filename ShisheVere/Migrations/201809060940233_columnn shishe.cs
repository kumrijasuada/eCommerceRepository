namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnnshishe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCart", "Shishe_Id_shishe", "dbo.Shishe");
            DropIndex("dbo.ShoppingCart", new[] { "Shishe_Id_shishe" });
            AddColumn("dbo.ShoppingCart", "Shishe", c => c.String());
            DropColumn("dbo.ShoppingCart", "Shishe_Id_shishe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "Shishe_Id_shishe", c => c.Int());
            DropColumn("dbo.ShoppingCart", "Shishe");
            CreateIndex("dbo.ShoppingCart", "Shishe_Id_shishe");
            AddForeignKey("dbo.ShoppingCart", "Shishe_Id_shishe", "dbo.Shishe", "Id_shishe");
        }
    }
}
