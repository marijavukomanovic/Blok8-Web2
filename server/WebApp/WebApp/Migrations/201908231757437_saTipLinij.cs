namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saTipLinij : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StanicaLinijas", "Stanica_Naziv", "dbo.Stanicas");
            DropForeignKey("dbo.StanicaLinijas", "Linija_RedBroj", "dbo.Linijas");
            DropIndex("dbo.StanicaLinijas", new[] { "Stanica_Naziv" });
            DropIndex("dbo.StanicaLinijas", new[] { "Linija_RedBroj" });
            CreateTable(
                "dbo.TipLinijes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Linijas", "TipId", c => c.Int(nullable: false));
            AddColumn("dbo.Linijas", "Aktivna", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stanicas", "Aktivna", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stanicas", "Linija_RedBroj", c => c.Int());
            CreateIndex("dbo.Linijas", "TipId");
            CreateIndex("dbo.Stanicas", "Linija_RedBroj");
            AddForeignKey("dbo.Stanicas", "Linija_RedBroj", "dbo.Linijas", "RedBroj");
            AddForeignKey("dbo.Linijas", "TipId", "dbo.TipLinijes", "Id", cascadeDelete: true);
            DropTable("dbo.StanicaLinijas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StanicaLinijas",
                c => new
                    {
                        Stanica_Naziv = c.String(nullable: false, maxLength: 128),
                        Linija_RedBroj = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Stanica_Naziv, t.Linija_RedBroj });
            
            DropForeignKey("dbo.Linijas", "TipId", "dbo.TipLinijes");
            DropForeignKey("dbo.Stanicas", "Linija_RedBroj", "dbo.Linijas");
            DropIndex("dbo.Stanicas", new[] { "Linija_RedBroj" });
            DropIndex("dbo.Linijas", new[] { "TipId" });
            DropColumn("dbo.Stanicas", "Linija_RedBroj");
            DropColumn("dbo.Stanicas", "Aktivna");
            DropColumn("dbo.Linijas", "Aktivna");
            DropColumn("dbo.Linijas", "TipId");
            DropTable("dbo.TipLinijes");
            CreateIndex("dbo.StanicaLinijas", "Linija_RedBroj");
            CreateIndex("dbo.StanicaLinijas", "Stanica_Naziv");
            AddForeignKey("dbo.StanicaLinijas", "Linija_RedBroj", "dbo.Linijas", "RedBroj", cascadeDelete: true);
            AddForeignKey("dbo.StanicaLinijas", "Stanica_Naziv", "dbo.Stanicas", "Naziv", cascadeDelete: true);
        }
    }
}
