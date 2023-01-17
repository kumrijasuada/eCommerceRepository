namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyAtNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Notification", "Id_shishe");
            AddForeignKey("dbo.Notification", "Id_shishe", "dbo.Shishe", "Id_shishe", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notification", "Id_shishe", "dbo.Shishe");
            DropIndex("dbo.Notification", new[] { "Id_shishe" });
        }
    }
}
