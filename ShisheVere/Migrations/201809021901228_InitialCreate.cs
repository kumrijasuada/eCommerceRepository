namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Foto",
                c => new
                    {
                        Id_foto = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                        File = c.String(nullable: false),
                        Id_shishe = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_foto)
                .ForeignKey("dbo.Shishe", t => t.Id_shishe, cascadeDelete: true)
                .Index(t => t.Id_shishe);
            
            CreateTable(
                "dbo.Shishe",
                c => new
                    {
                        Id_shishe = c.Int(nullable: false, identity: true),
                        Emertim = c.String(nullable: false),
                        Kapacitet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pesha = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gjatesia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Diametri = c.Decimal(nullable: false, precision: 18, scale: 2),
                        id_kategori = c.Int(),
                        id_prodhues = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_shishe)
                .ForeignKey("dbo.Kategori", t => t.id_kategori)
                .ForeignKey("dbo.Prodhues", t => t.id_prodhues, cascadeDelete: true)
                .Index(t => t.id_kategori)
                .Index(t => t.id_prodhues);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Id_kategori = c.Int(nullable: false, identity: true),
                        Emertim = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_kategori);
            
            CreateTable(
                "dbo.Prodhues",
                c => new
                    {
                        Id_prodhues = c.Int(nullable: false, identity: true),
                        Emertim = c.String(nullable: false),
                        Adrese = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_prodhues);
            
            CreateTable(
                "dbo.Perdorues",
                c => new
                    {
                        Id_perdorues = c.Int(nullable: false, identity: true),
                        Emer = c.String(nullable: false),
                        Mbiemer = c.String(nullable: false),
                        Adrese = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        telefon = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_perdorues);
            
            CreateTable(
                "dbo.Roli",
                c => new
                    {
                        Id_roli = c.Int(nullable: false, identity: true),
                        Emertim = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_roli);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Perdorues", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roli", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roli");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Perdorues");
            DropForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues");
            DropForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori");
            DropForeignKey("dbo.Foto", "Id_shishe", "dbo.Shishe");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Shishe", new[] { "id_prodhues" });
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            DropIndex("dbo.Foto", new[] { "Id_shishe" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roli");
            DropTable("dbo.Perdorues");
            DropTable("dbo.Prodhues");
            DropTable("dbo.Kategori");
            DropTable("dbo.Shishe");
            DropTable("dbo.Foto");
        }
    }
}
