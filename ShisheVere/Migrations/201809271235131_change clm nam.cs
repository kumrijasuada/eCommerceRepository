namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeclmnam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "id_shishe", c => c.Int(nullable: false));
            DropColumn("dbo.Notification", "id_prodhues");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notification", "id_prodhues", c => c.Int(nullable: false));
            DropColumn("dbo.Notification", "id_shishe");
        }
    }
}
