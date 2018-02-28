namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSprintTaskTable3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProjectUsers", new[] { "ProjectID" });
            DropIndex("dbo.ProjectUsers", new[] { "UserID" });
            CreateIndex("dbo.ProjectUsers", "ProjectId");
            CreateIndex("dbo.ProjectUsers", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            DropIndex("dbo.ProjectUsers", new[] { "ProjectId" });
            CreateIndex("dbo.ProjectUsers", "UserID");
            CreateIndex("dbo.ProjectUsers", "ProjectID");
        }
    }
}
