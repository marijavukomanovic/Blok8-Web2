namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Karte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kartas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PocetakVazenja = c.DateTime(nullable: false),
                        ZavrsetakVazenja = c.DateTime(nullable: false),
                        Naziv_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VrstaKartes", t => t.Naziv_Id)
                .Index(t => t.Naziv_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kartas", "Naziv_Id", "dbo.VrstaKartes");
            DropIndex("dbo.Kartas", new[] { "Naziv_Id" });
            DropTable("dbo.Kartas");
        }
    }
}
