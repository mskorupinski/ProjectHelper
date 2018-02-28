namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSprintTaskTable5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SprintNumber = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.SprintTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Note = c.Int(nullable: false),
                        Status = c.Int(),
                        SprintId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SprintId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SprintTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.SprintTasks", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.Sprints", "ProjectId", "dbo.Projects");
            DropIndex("dbo.SprintTasks", new[] { "UserId" });
            DropIndex("dbo.SprintTasks", new[] { "SprintId" });
            DropIndex("dbo.Sprints", new[] { "ProjectId" });
            DropTable("dbo.SprintTasks");
            DropTable("dbo.Sprints");
        }
    }
}
