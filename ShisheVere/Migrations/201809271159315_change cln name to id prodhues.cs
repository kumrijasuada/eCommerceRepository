namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeclnnametoidprodhues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification", "id_prodhues", c => c.Int(nullable: false));
            DropColumn("dbo.Notification", "id_shishe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notification", "id_shishe", c => c.Int(nullable: false));
            DropColumn("dbo.Notification", "id_prodhues");
        }
    }
}
