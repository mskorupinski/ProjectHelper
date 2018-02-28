namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTaskUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SprintTaskID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SprintTasks", t => t.SprintTaskID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.SprintTaskID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.TaskUsers", "SprintTaskID", "dbo.SprintTasks");
            DropIndex("dbo.TaskUsers", new[] { "UserID" });
            DropIndex("dbo.TaskUsers", new[] { "SprintTaskID" });
            DropTable("dbo.TaskUsers");
        }
    }
}
