namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shishetbl : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Shishe", "Emertim");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shishe", "Emertim", c => c.String(nullable: false));
        }
    }
}
