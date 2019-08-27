namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinijaPolje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Linijas", "Opis", c => c.String());
            AddColumn("dbo.Linijas", "Boja", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Linijas", "Boja");
            DropColumn("dbo.Linijas", "Opis");
        }
    }
}
