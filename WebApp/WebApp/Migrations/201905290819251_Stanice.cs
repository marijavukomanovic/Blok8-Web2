namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Stanice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        Naziv = c.String(nullable: false, maxLength: 128),
                        Adresa = c.String(),
                        GeografskeKoordinataX = c.Double(nullable: false),
                        GeografskeKoordinataY = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Naziv);
            
            CreateTable(
                "dbo.Linijas",
                c => new
                    {
                        RedBroj = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RedBroj);
            
            CreateTable(
                "dbo.LinijaStanicas",
                c => new
                    {
                        Linija_RedBroj = c.Int(nullable: false),
                        Stanica_Naziv = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Linija_RedBroj, t.Stanica_Naziv })
                .ForeignKey("dbo.Linijas", t => t.Linija_RedBroj, cascadeDelete: true)
                .ForeignKey("dbo.Stanicas", t => t.Stanica_Naziv, cascadeDelete: true)
                .Index(t => t.Linija_RedBroj)
                .Index(t => t.Stanica_Naziv);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinijaStanicas", "Stanica_Naziv", "dbo.Stanicas");
            DropForeignKey("dbo.LinijaStanicas", "Linija_RedBroj", "dbo.Linijas");
            DropIndex("dbo.LinijaStanicas", new[] { "Stanica_Naziv" });
            DropIndex("dbo.LinijaStanicas", new[] { "Linija_RedBroj" });
            DropTable("dbo.LinijaStanicas");
            DropTable("dbo.Linijas");
            DropTable("dbo.Stanicas");
        }
    }
}
