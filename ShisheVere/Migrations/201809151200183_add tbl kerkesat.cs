namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtblkerkesat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kerkesat",
                c => new
                    {
                        kerkesaId = c.String(nullable: false, maxLength: 128),
                        perdoruesId = c.Int(nullable: false),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.kerkesaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Kerkesat");
        }
    }
}
