namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users");
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            AddColumn("dbo.ProjectUsers", "User_Id", c => c.Int());
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.String());
            CreateIndex("dbo.ProjectUsers", "User_Id");
            AddForeignKey("dbo.ProjectUsers", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.ProjectUsers", "User_Id");
            CreateIndex("dbo.ProjectUsers", "UserId");
            AddForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
