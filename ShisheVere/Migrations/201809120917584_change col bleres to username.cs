namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecolblerestousername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShoppingCart", "UserName", c => c.String());
            DropColumn("dbo.ShoppingCart", "Bleres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShoppingCart", "Bleres", c => c.String());
            DropColumn("dbo.ShoppingCart", "UserName");
        }
    }
}
