namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shishe", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shishe", "status");
        }
    }
}
