namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodatakAktivnihPolja11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Kartas", "Verifikovana", c => c.Boolean(nullable: false));
            AddColumn("dbo.Korisniks", "StatusId", c => c.Int(nullable: false));
            AddColumn("dbo.RedVoznjes", "Aktivan", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Korisniks", "StatusId");
            AddForeignKey("dbo.Korisniks", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisniks", "StatusId", "dbo.Status");
            DropIndex("dbo.Korisniks", new[] { "StatusId" });
            DropColumn("dbo.RedVoznjes", "Aktivan");
            DropColumn("dbo.Korisniks", "StatusId");
            DropColumn("dbo.Kartas", "Verifikovana");
            DropTable("dbo.Status");
        }
    }
}
