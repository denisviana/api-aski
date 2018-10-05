namespace api_aski.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discipline",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Course_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                        User_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.User_Id1)
                .Index(t => t.Course_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        WantBeHelped = c.Boolean(nullable: false),
                        WantToHelp = c.Boolean(nullable: false),
                        Discipline_Id = c.String(maxLength: 128),
                        Discipline_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discipline", t => t.Discipline_Id)
                .ForeignKey("dbo.Discipline", t => t.Discipline_Id1)
                .Index(t => t.Discipline_Id)
                .Index(t => t.Discipline_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "Discipline_Id1", "dbo.Discipline");
            DropForeignKey("dbo.User", "Discipline_Id", "dbo.Discipline");
            DropForeignKey("dbo.Discipline", "User_Id1", "dbo.User");
            DropForeignKey("dbo.Discipline", "User_Id", "dbo.User");
            DropForeignKey("dbo.Discipline", "Course_Id", "dbo.Courses");
            DropIndex("dbo.User", new[] { "Discipline_Id1" });
            DropIndex("dbo.User", new[] { "Discipline_Id" });
            DropIndex("dbo.Discipline", new[] { "User_Id1" });
            DropIndex("dbo.Discipline", new[] { "User_Id" });
            DropIndex("dbo.Discipline", new[] { "Course_Id" });
            DropTable("dbo.User");
            DropTable("dbo.Courses");
            DropTable("dbo.Discipline");
        }
    }
}
