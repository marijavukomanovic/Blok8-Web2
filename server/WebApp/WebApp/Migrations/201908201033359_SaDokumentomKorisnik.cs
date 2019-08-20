namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaDokumentomKorisnik : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Korisniks", "Document", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Korisniks", "Document");
        }
    }
}
