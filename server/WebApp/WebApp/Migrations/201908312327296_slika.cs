namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class slika : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Korisniks", "Document", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Korisniks", "Document", c => c.Binary());
        }
    }
}
