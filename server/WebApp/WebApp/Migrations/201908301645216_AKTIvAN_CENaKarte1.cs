namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AKTIvAN_CENaKarte1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CenaKartes", "Aktivan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CenaKartes", "Aktivan");
        }
    }
}
