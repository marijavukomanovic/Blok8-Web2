namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinijaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.LinijaId, cascadeDelete: true)
                .Index(t => t.LinijaId);
            
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
                        GeografskeKoordinataX = c.Double(nullable: false),
                        GeografskeKoordinataY = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Naziv);
            
            CreateTable(
                "dbo.CenaKartes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CenovnikId = c.Int(nullable: false),
                        TipKarteId = c.Int(nullable: false),
                        Cena = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cenovniks", t => t.CenovnikId, cascadeDelete: true)
                .ForeignKey("dbo.TipKartes", t => t.TipKarteId, cascadeDelete: true)
                .Index(t => t.CenovnikId)
                .Index(t => t.TipKarteId);
            
            CreateTable(
                "dbo.Cenovniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VazenjeOd = c.DateTime(nullable: false),
                        VazenjeDo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipKartes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CenaKarteId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        VremeKupovine = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.CenaKartes", t => t.CenaKarteId, cascadeDelete: true)
                .Index(t => t.CenaKarteId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.TipPutnikas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Koeficijent = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RedVoznjes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RasporedVoznje = c.String(),
                        LinijaId = c.Int(nullable: false),
                        TipDanaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.LinijaId, cascadeDelete: true)
                .ForeignKey("dbo.TipDanas", t => t.TipDanaId, cascadeDelete: true)
                .Index(t => t.LinijaId)
                .Index(t => t.TipDanaId);
            
            CreateTable(
                "dbo.TipDanas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.String(),
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
            
            AddColumn("dbo.AspNetUsers", "TipPutnikaId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "TipPutnikaId");
            AddForeignKey("dbo.AspNetUsers", "TipPutnikaId", "dbo.TipPutnikas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RedVoznjes", "TipDanaId", "dbo.TipDanas");
            DropForeignKey("dbo.RedVoznjes", "LinijaId", "dbo.Linijas");
            DropForeignKey("dbo.Kartas", "CenaKarteId", "dbo.CenaKartes");
            DropForeignKey("dbo.Kartas", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "TipPutnikaId", "dbo.TipPutnikas");
            DropForeignKey("dbo.CenaKartes", "TipKarteId", "dbo.TipKartes");
            DropForeignKey("dbo.CenaKartes", "CenovnikId", "dbo.Cenovniks");
            DropForeignKey("dbo.Autobus", "LinijaId", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Linija_RedBroj", "dbo.Linijas");
            DropForeignKey("dbo.StanicaLinijas", "Stanica_Naziv", "dbo.Stanicas");
            DropIndex("dbo.StanicaLinijas", new[] { "Linija_RedBroj" });
            DropIndex("dbo.StanicaLinijas", new[] { "Stanica_Naziv" });
            DropIndex("dbo.RedVoznjes", new[] { "TipDanaId" });
            DropIndex("dbo.RedVoznjes", new[] { "LinijaId" });
            DropIndex("dbo.AspNetUsers", new[] { "TipPutnikaId" });
            DropIndex("dbo.Kartas", new[] { "ApplicationUserId" });
            DropIndex("dbo.Kartas", new[] { "CenaKarteId" });
            DropIndex("dbo.CenaKartes", new[] { "TipKarteId" });
            DropIndex("dbo.CenaKartes", new[] { "CenovnikId" });
            DropIndex("dbo.Autobus", new[] { "LinijaId" });
            DropColumn("dbo.AspNetUsers", "TipPutnikaId");
            DropTable("dbo.StanicaLinijas");
            DropTable("dbo.TipDanas");
            DropTable("dbo.RedVoznjes");
            DropTable("dbo.TipPutnikas");
            DropTable("dbo.Kartas");
            DropTable("dbo.TipKartes");
            DropTable("dbo.Cenovniks");
            DropTable("dbo.CenaKartes");
            DropTable("dbo.Stanicas");
            DropTable("dbo.Linijas");
            DropTable("dbo.Autobus");
        }
    }
}
