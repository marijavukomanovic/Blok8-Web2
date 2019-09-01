namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nevalja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Korisniks", "Document", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Korisniks", "Document", c => c.String());
        }
    }
}
