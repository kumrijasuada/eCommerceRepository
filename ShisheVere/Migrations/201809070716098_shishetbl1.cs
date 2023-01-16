namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shishetbl1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shishe", "Emertim", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shishe", "Emertim");
        }
    }
}
