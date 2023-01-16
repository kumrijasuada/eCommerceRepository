namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclmprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shishe", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shishe", "Price");
        }
    }
}
