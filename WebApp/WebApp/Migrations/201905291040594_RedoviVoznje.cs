namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RedoviVoznje : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RedVoznjes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Linija_RedBroj = c.Int(),
                        LinijaId_RedBroj = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Linijas", t => t.Linija_RedBroj)
                .ForeignKey("dbo.Linijas", t => t.LinijaId_RedBroj)
                .Index(t => t.Linija_RedBroj)
                .Index(t => t.LinijaId_RedBroj);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RedVoznjes", "LinijaId_RedBroj", "dbo.Linijas");
            DropForeignKey("dbo.RedVoznjes", "Linija_RedBroj", "dbo.Linijas");
            DropIndex("dbo.RedVoznjes", new[] { "LinijaId_RedBroj" });
            DropIndex("dbo.RedVoznjes", new[] { "Linija_RedBroj" });
            DropTable("dbo.RedVoznjes");
        }
    }
}
