namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Perdorues");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roli");
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            AddColumn("dbo.Perdorues", "Roli", c => c.String());
            DropTable("dbo.Roli");
            DropTable("dbo.UserRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.Roli",
                c => new
                    {
                        Id_roli = c.Int(nullable: false, identity: true),
                        Emertim = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_roli);
            
            DropColumn("dbo.Perdorues", "Roli");
            CreateIndex("dbo.UserRoles", "RoleId");
            CreateIndex("dbo.UserRoles", "UserId");
            AddForeignKey("dbo.UserRoles", "RoleId", "dbo.Roli", "Id_roli", cascadeDelete: true);
            AddForeignKey("dbo.UserRoles", "UserId", "dbo.Perdorues", "Id_perdorues", cascadeDelete: true);
        }
    }
}
