namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumntelefoni : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Telefoni", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Telefoni");
        }
    }
}
