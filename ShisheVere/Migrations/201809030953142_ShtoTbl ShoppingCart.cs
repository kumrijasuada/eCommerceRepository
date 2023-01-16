namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShtoTblShoppingCart : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShoppingCart");
        }
    }
}
