namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BezTipKarteId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VrstaKartes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VrstaKartes");
        }
    }
}
