namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Korisniks", "Tip_Id", "dbo.TipPutnikas");
            DropIndex("dbo.Korisniks", new[] { "Tip_Id" });
            RenameColumn(table: "dbo.Korisniks", name: "Tip_Id", newName: "TipId");
            AlterColumn("dbo.Korisniks", "TipId", c => c.Int(nullable: false));
            CreateIndex("dbo.Korisniks", "TipId");
            AddForeignKey("dbo.Korisniks", "TipId", "dbo.TipPutnikas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Korisniks", "TipId", "dbo.TipPutnikas");
            DropIndex("dbo.Korisniks", new[] { "TipId" });
            AlterColumn("dbo.Korisniks", "TipId", c => c.Int());
            RenameColumn(table: "dbo.Korisniks", name: "TipId", newName: "Tip_Id");
            CreateIndex("dbo.Korisniks", "Tip_Id");
            AddForeignKey("dbo.Korisniks", "Tip_Id", "dbo.TipPutnikas", "Id");
        }
    }
}
