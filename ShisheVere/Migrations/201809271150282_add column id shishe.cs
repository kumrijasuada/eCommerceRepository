namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnidshishe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "id_shishe", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification", "id_shishe");
        }
    }
}
