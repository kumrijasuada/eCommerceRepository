namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteconstrainrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shishe", "Emertim", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shishe", "Emertim", c => c.String(nullable: false));
        }
    }
}
