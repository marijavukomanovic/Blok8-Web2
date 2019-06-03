namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Linije : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.LinijaStanicas", newName: "StanicaLinijas");
            DropPrimaryKey("dbo.StanicaLinijas");
            AddPrimaryKey("dbo.StanicaLinijas", new[] { "Stanica_Naziv", "Linija_RedBroj" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StanicaLinijas");
            AddPrimaryKey("dbo.StanicaLinijas", new[] { "Linija_RedBroj", "Stanica_Naziv" });
            RenameTable(name: "dbo.StanicaLinijas", newName: "LinijaStanicas");
        }
    }
}
