namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tdsg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues");
            DropPrimaryKey("dbo.Prodhues");
            AlterColumn("dbo.Prodhues", "Id_prodhues", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Prodhues", "Id_prodhues");
            AddForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues", "Id_prodhues", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues");
            DropPrimaryKey("dbo.Prodhues");
            AlterColumn("dbo.Prodhues", "Id_prodhues", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Prodhues", "Id_prodhues");
            AddForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues", "Id_prodhues", cascadeDelete: true);
        }
    }
}
