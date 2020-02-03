namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payPalMigracija : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayPals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PayementId = c.String(),
                        CreateTime = c.DateTime(),
                        PayerEmail = c.String(),
                        PayerName = c.String(),
                        PayerSurname = c.String(),
                        CurrencyCode = c.String(),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Kartas", "PayPalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Kartas", "PayPalId");
            AddForeignKey("dbo.Kartas", "PayPalId", "dbo.PayPals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kartas", "PayPalId", "dbo.PayPals");
            DropIndex("dbo.Kartas", new[] { "PayPalId" });
            DropColumn("dbo.Kartas", "PayPalId");
            DropTable("dbo.PayPals");
        }
    }
}
