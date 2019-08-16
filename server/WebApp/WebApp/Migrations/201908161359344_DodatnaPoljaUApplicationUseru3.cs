namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodatnaPoljaUApplicationUseru3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "TipPutnikaId", "dbo.TipPutnikas");
            DropIndex("dbo.AspNetUsers", new[] { "TipPutnikaId" });
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
                        Tip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipPutnikas", t => t.Tip_Id)
                .Index(t => t.Tip_Id);
            
            AddColumn("dbo.AspNetUsers", "KorisnikId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "KorisnikId");
            AddForeignKey("dbo.AspNetUsers", "KorisnikId", "dbo.Korisniks", "Id");
            DropColumn("dbo.AspNetUsers", "TipPutnikaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TipPutnikaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AspNetUsers", "KorisnikId", "dbo.Korisniks");
            DropForeignKey("dbo.Korisniks", "Tip_Id", "dbo.TipPutnikas");
            DropIndex("dbo.Korisniks", new[] { "Tip_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "KorisnikId" });
            DropColumn("dbo.AspNetUsers", "KorisnikId");
            DropTable("dbo.Korisniks");
            CreateIndex("dbo.AspNetUsers", "TipPutnikaId");
            AddForeignKey("dbo.AspNetUsers", "TipPutnikaId", "dbo.TipPutnikas", "Id", cascadeDelete: true);
        }
    }
}
