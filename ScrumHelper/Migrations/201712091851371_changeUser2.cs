namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUser2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProjectUsers", "User_Id", "dbo.Users");
            DropIndex("dbo.ProjectUsers", new[] { "User_Id" });
            DropColumn("dbo.ProjectUsers", "UserId");
            RenameColumn(table: "dbo.ProjectUsers", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectUsers", "UserId");
            AddForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectUsers", "UserId", "dbo.Users");
            DropIndex("dbo.ProjectUsers", new[] { "UserId" });
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.Int());
            AlterColumn("dbo.ProjectUsers", "UserId", c => c.String());
            RenameColumn(table: "dbo.ProjectUsers", name: "UserId", newName: "User_Id");
            AddColumn("dbo.ProjectUsers", "UserId", c => c.String());
            CreateIndex("dbo.ProjectUsers", "User_Id");
            AddForeignKey("dbo.ProjectUsers", "User_Id", "dbo.Users", "Id");
        }
    }
}
