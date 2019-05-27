namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutobusIvremenskaKarta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pozicija = c.Int(nullable: false),
                        BrojLinije_RedBroj = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.BrojLinije_RedBroj)
                .Index(t => t.BrojLinije_RedBroj);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        RedBroj = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RedBroj);
            
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        Naziv = c.String(nullable: false, maxLength: 128),
                        Adresa = c.String(),
                        GeografskeKoordinate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Naziv);
            
            CreateTable(
                "dbo.VremenskaKartas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VremeCekiranja = c.DateTime(nullable: false),
                        Cena = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StanicaLinijas",
                c => new
                    {
                        Stanica_Naziv = c.String(nullable: false, maxLength: 128),
                        Linija_RedBroj = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Stanica_Naziv, t.Linija_RedBroj })
                .ForeignKey("dbo.Stanicas", t => t.Stanica_Naziv, cascadeDelete: true)
                .ForeignKey("dbo.Linijas", t => t.Linija_RedBroj, cascadeDelete: true)
                .Index(t => t.Stanica_Naziv)
                .Index(t => t.Linija_RedBroj);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Autobus", "BrojLinije_RedBroj", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Linija_RedBroj", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Stanica_Naziv", "dbo.Stanicas");
            DropIndex("dbo.StanicaLinijas", new[] { "Linija_RedBroj" });
            DropIndex("dbo.StanicaLinijas", new[] { "Stanica_Naziv" });
            DropIndex("dbo.Autobus", new[] { "BrojLinije_RedBroj" });
            DropTable("dbo.StanicaLinijas");
            DropTable("dbo.VremenskaKartas");
            DropTable("dbo.Stanicas");
            DropTable("dbo.Linijas");
            DropTable("dbo.Autobus");
        }
    }
}
