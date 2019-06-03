namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Autobus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Autobus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrojLinijeId = c.Int(nullable: false),
                        GeografskeKoordinataX = c.Double(nullable: false),
                        GeografskeKoordinataY = c.Double(nullable: false),
                        BrojLinije_RedBroj = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.BrojLinije_RedBroj)
                .Index(t => t.BrojLinije_RedBroj);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Autobus", "BrojLinije_RedBroj", "dbo.Linijas");
            DropIndex("dbo.Autobus", new[] { "BrojLinije_RedBroj" });
            DropTable("dbo.Autobus");
        }
    }
}
