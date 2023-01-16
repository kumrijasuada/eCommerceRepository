namespace ShisheVere.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Roliisrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Perdorues", "Roli", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Perdorues", "Roli", c => c.String());
        }
    }
}
