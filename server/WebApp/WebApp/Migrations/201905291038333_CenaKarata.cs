namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CenaKarata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CenaKartes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CenovnikId = c.Int(nullable: false),
                        VrstaKarte_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cenovniks", t => t.CenovnikId, cascadeDelete: true)
                .ForeignKey("dbo.VrstaKartes", t => t.VrstaKarte_Id)
                .Index(t => t.CenovnikId)
                .Index(t => t.VrstaKarte_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CenaKartes", "VrstaKarte_Id", "dbo.VrstaKartes");
            DropForeignKey("dbo.CenaKartes", "CenovnikId", "dbo.Cenovniks");
            DropIndex("dbo.CenaKartes", new[] { "VrstaKarte_Id" });
            DropIndex("dbo.CenaKartes", new[] { "CenovnikId" });
            DropTable("dbo.CenaKartes");
        }
    }
}
