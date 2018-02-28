namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "UserID", "dbo.Users");
            DropForeignKey("dbo.ProjectUsers", "ProjectID", "dbo.Projects");
            DropIndex("dbo.ProjectUsers", new[] { "UserID" });
            DropIndex("dbo.ProjectUsers", new[] { "ProjectID" });
            DropTable("dbo.ProjectUsers");
        }
    }
}
