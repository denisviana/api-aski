namespace api_aski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(),
                        Points = c.Int(nullable: false),
                        Discipline_Id = c.String(maxLength: 128),
                        WhoAsks_Id = c.String(maxLength: 128),
                        WhoResponds_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discipline", t => t.Discipline_Id)
                .ForeignKey("dbo.User", t => t.WhoAsks_Id)
                .ForeignKey("dbo.User", t => t.WhoResponds_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.User_Id1)
                .Index(t => t.Discipline_Id)
                .Index(t => t.WhoAsks_Id)
                .Index(t => t.WhoResponds_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "User_Id1", "dbo.User");
            DropForeignKey("dbo.Questions", "User_Id", "dbo.User");
            DropForeignKey("dbo.Questions", "WhoResponds_Id", "dbo.User");
            DropForeignKey("dbo.Questions", "WhoAsks_Id", "dbo.User");
            DropForeignKey("dbo.Questions", "Discipline_Id", "dbo.Discipline");
            DropIndex("dbo.Questions", new[] { "User_Id1" });
            DropIndex("dbo.Questions", new[] { "User_Id" });
            DropIndex("dbo.Questions", new[] { "WhoResponds_Id" });
            DropIndex("dbo.Questions", new[] { "WhoAsks_Id" });
            DropIndex("dbo.Questions", new[] { "Discipline_Id" });
            DropTable("dbo.Questions");
        }
    }
}
