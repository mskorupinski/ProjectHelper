namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUsersTask : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SprintTasks", "UserId", "dbo.Users");
            DropIndex("dbo.SprintTasks", new[] { "UserId" });
            AddColumn("dbo.Users", "SprintTask_Id", c => c.Int());
            CreateIndex("dbo.Users", "SprintTask_Id");
            AddForeignKey("dbo.Users", "SprintTask_Id", "dbo.SprintTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "SprintTask_Id", "dbo.SprintTasks");
            DropIndex("dbo.Users", new[] { "SprintTask_Id" });
            DropColumn("dbo.Users", "SprintTask_Id");
            CreateIndex("dbo.SprintTasks", "UserId");
            AddForeignKey("dbo.SprintTasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
