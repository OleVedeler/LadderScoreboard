namespace DotNetAppSqlDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLadderEntry : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LadderEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ArmyList = c.String(),
                        Army = c.String(),
                        PlayerName = c.String(),
                        Rank = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Todoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Todoes");
            DropTable("dbo.LadderEntries");
        }
    }
}
