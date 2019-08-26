namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sveee : DbMigration
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
                        Id = c.Int(nullable: false, identity: true),
                        RedBroj = c.String(),
                        TipId = c.Int(nullable: false),
                        Aktivna = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipLinijes", t => t.TipId, cascadeDelete: true)
                .Index(t => t.TipId);
            
            CreateTable(
                "dbo.TipLinijes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Korisniks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KorisnickoIme = c.String(),
                        Ime = c.String(),
                        Prezime = c.String(),
                        Email = c.String(),
                        Sifra = c.String(),
                        DatumRodjenja = c.DateTime(nullable: false),
                        Adresa = c.String(),
                        TipId = c.Int(nullable: false),
                        Document = c.Binary(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipPutnikas", t => t.TipId, cascadeDelete: true)
                .Index(t => t.TipId);
            
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
                "dbo.LinijeStanices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinijeId = c.Int(nullable: false),
                        StaniceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.LinijeId, cascadeDelete: true)
                .ForeignKey("dbo.Stanicas", t => t.StaniceId, cascadeDelete: true)
                .Index(t => t.LinijeId)
                .Index(t => t.StaniceId);
            
            CreateTable(
                "dbo.Stanicas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                        Adresa = c.String(),
                        GeografskeKoordinataX = c.Double(nullable: false),
                        GeografskeKoordinataY = c.Double(nullable: false),
                        Aktivna = c.Boolean(nullable: false),
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
            
            AddColumn("dbo.AspNetUsers", "KorisnikId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "KorisnikId");
            AddForeignKey("dbo.AspNetUsers", "KorisnikId", "dbo.Korisniks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RedVoznjes", "TipDanaId", "dbo.TipDanas");
            DropForeignKey("dbo.RedVoznjes", "LinijaId", "dbo.Linijas");
            DropForeignKey("dbo.LinijeStanices", "StaniceId", "dbo.Stanicas");
            DropForeignKey("dbo.LinijeStanices", "LinijeId", "dbo.Linijas");
            DropForeignKey("dbo.Kartas", "CenaKarteId", "dbo.CenaKartes");
            DropForeignKey("dbo.Kartas", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "KorisnikId", "dbo.Korisniks");
            DropForeignKey("dbo.Korisniks", "TipId", "dbo.TipPutnikas");
            DropForeignKey("dbo.CenaKartes", "TipKarteId", "dbo.TipKartes");
            DropForeignKey("dbo.CenaKartes", "CenovnikId", "dbo.Cenovniks");
            DropForeignKey("dbo.Autobus", "LinijaId", "dbo.Linijas");
            DropForeignKey("dbo.Linijas", "TipId", "dbo.TipLinijes");
            DropIndex("dbo.RedVoznjes", new[] { "TipDanaId" });
            DropIndex("dbo.RedVoznjes", new[] { "LinijaId" });
            DropIndex("dbo.LinijeStanices", new[] { "StaniceId" });
            DropIndex("dbo.LinijeStanices", new[] { "LinijeId" });
            DropIndex("dbo.Korisniks", new[] { "TipId" });
            DropIndex("dbo.AspNetUsers", new[] { "KorisnikId" });
            DropIndex("dbo.Kartas", new[] { "ApplicationUserId" });
            DropIndex("dbo.Kartas", new[] { "CenaKarteId" });
            DropIndex("dbo.CenaKartes", new[] { "TipKarteId" });
            DropIndex("dbo.CenaKartes", new[] { "CenovnikId" });
            DropIndex("dbo.Linijas", new[] { "TipId" });
            DropIndex("dbo.Autobus", new[] { "LinijaId" });
            DropColumn("dbo.AspNetUsers", "KorisnikId");
            DropTable("dbo.TipDanas");
            DropTable("dbo.RedVoznjes");
            DropTable("dbo.Stanicas");
            DropTable("dbo.LinijeStanices");
            DropTable("dbo.TipPutnikas");
            DropTable("dbo.Korisniks");
            DropTable("dbo.Kartas");
            DropTable("dbo.TipKartes");
            DropTable("dbo.Cenovniks");
            DropTable("dbo.CenaKartes");
            DropTable("dbo.TipLinijes");
            DropTable("dbo.Linijas");
            DropTable("dbo.Autobus");
        }
    }
}
