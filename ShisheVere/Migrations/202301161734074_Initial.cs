namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Emertim = c.String(),
                        Kapacitet = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pesha = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Gjatesia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Diametri = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sasia = c.Int(nullable: false),
                        id_kategori = c.Int(nullable: false),
                        id_prodhues = c.Int(nullable: false),
                        status = c.String(),
                    })
                .PrimaryKey(t => t.Id_shishe)
                .ForeignKey("dbo.Kategori", t => t.id_kategori, cascadeDelete: true)
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
                "dbo.Kerkesat",
                c => new
                    {
                        kerkesaId = c.String(nullable: false, maxLength: 128),
                        perdoruesId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.kerkesaId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        action = c.String(),
                        id_shishe = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Id_shishe = c.Int(nullable: false),
                        Id_perdorues = c.Int(nullable: false),
                        Emri = c.String(),
                        Mbiemri = c.String(),
                        Adresa = c.String(),
                        Telefoni = c.String(),
                        Sasia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransaksionID = c.String(),
                        Pagesa = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Statusi_Pagese = c.String(),
                        ShisheID = c.Int(nullable: false),
                        Ora = c.DateTime(nullable: false),
                        Valuta = c.String(),
                        SasiaPorosi = c.Int(nullable: false),
                        EmriShishe = c.String(),
                        EmailBleres = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perdorues",
                c => new
                    {
                        Id_perdorues = c.Int(nullable: false, identity: true),
                        Emer = c.String(nullable: false),
                        Mbiemer = c.String(nullable: false),
                        Adrese = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Salt = c.Binary(nullable: false),
                        Password = c.Binary(nullable: false),
                        Roli = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id_perdorues);
            
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Id_shishe = c.Int(nullable: false),
                        Id_perdorues = c.Int(nullable: false),
                        UserName = c.String(),
                        Shishe = c.String(),
                        Sasia = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        foto = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shishe", "id_prodhues", "dbo.Prodhues");
            DropForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori");
            DropForeignKey("dbo.Foto", "Id_shishe", "dbo.Shishe");
            DropIndex("dbo.Shishe", new[] { "id_prodhues" });
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            DropIndex("dbo.Foto", new[] { "Id_shishe" });
            DropTable("dbo.ShoppingCart");
            DropTable("dbo.Perdorues");
            DropTable("dbo.Payments");
            DropTable("dbo.Orders");
            DropTable("dbo.Notification");
            DropTable("dbo.Kerkesat");
            DropTable("dbo.Prodhues");
            DropTable("dbo.Kategori");
            DropTable("dbo.Shishe");
            DropTable("dbo.Foto");
        }
    }
}
