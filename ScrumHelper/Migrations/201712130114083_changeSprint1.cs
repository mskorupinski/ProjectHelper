namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSprint1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sprints", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Sprints", new[] { "ProjectId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Sprints", "ProjectId");
            AddForeignKey("dbo.Sprints", "ProjectId", "dbo.Projects", "ID", cascadeDelete: true);
        }
    }
}
