namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columnrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Emri", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Mbiemri", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Adresa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Adresa", c => c.String());
            AlterColumn("dbo.Orders", "Mbiemri", c => c.String());
            AlterColumn("dbo.Orders", "Emri", c => c.String());
        }
    }
}
