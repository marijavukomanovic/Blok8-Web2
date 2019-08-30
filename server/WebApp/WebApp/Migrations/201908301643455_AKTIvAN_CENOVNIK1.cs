namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AKTIvAN_CENOVNIK1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cenovniks", "Aktivan", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cenovniks", "Aktivan");
        }
    }
}
