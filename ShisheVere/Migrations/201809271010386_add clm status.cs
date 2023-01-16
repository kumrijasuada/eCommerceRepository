namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclmstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification", "status");
        }
    }
}
