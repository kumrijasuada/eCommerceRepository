namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makekategorireqiured : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori");
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            AlterColumn("dbo.Shishe", "id_kategori", c => c.Int(nullable: false));
            CreateIndex("dbo.Shishe", "id_kategori");
            AddForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori", "Id_kategori", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori");
            DropIndex("dbo.Shishe", new[] { "id_kategori" });
            AlterColumn("dbo.Shishe", "id_kategori", c => c.Int());
            CreateIndex("dbo.Shishe", "id_kategori");
            AddForeignKey("dbo.Shishe", "id_kategori", "dbo.Kategori", "Id_kategori");
        }
    }
}
