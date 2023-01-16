namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Shishe = c.String(),
                        Emri = c.String(),
                        Mbiemri = c.String(),
                        Adresa = c.String(),
                        Sasia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.ShoppingCart");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Shishe = c.String(),
                        Bleres = c.String(),
                        Sasia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropTable("dbo.Orders");
        }
    }
}
