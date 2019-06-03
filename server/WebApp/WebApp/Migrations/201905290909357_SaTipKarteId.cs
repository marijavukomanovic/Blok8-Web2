namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaTipKarteId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VrstaKartes", "TipKarteId", c => c.Int(nullable: false));
            CreateIndex("dbo.VrstaKartes", "TipKarteId");
            AddForeignKey("dbo.VrstaKartes", "TipKarteId", "dbo.TipKartes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VrstaKartes", "TipKarteId", "dbo.TipKartes");
            DropIndex("dbo.VrstaKartes", new[] { "TipKarteId" });
            DropColumn("dbo.VrstaKartes", "TipKarteId");
        }
    }
}
