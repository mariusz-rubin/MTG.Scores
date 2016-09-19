namespace MTG.Scores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Player1ID = c.Int(nullable: false),
                        Player2ID = c.Int(nullable: false),
                        Player1Score = c.Int(nullable: false),
                        Player2Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Player", t => t.Player1ID)
                .ForeignKey("dbo.Player", t => t.Player2ID)
                .Index(t => t.Player1ID)
                .Index(t => t.Player2ID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "Player2ID", "dbo.Player");
            DropForeignKey("dbo.Match", "Player1ID", "dbo.Player");
            DropIndex("dbo.Match", new[] { "Player2ID" });
            DropIndex("dbo.Match", new[] { "Player1ID" });
            DropTable("dbo.Player");
            DropTable("dbo.Match");
        }
    }
}
