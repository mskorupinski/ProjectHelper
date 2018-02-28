namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "Project_ID", c => c.Int());
            CreateIndex("dbo.Users", "Project_ID");
            AddForeignKey("dbo.Users", "Project_ID", "dbo.Projects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Project_ID", "dbo.Projects");
            DropIndex("dbo.Users", new[] { "Project_ID" });
            DropColumn("dbo.Users", "Project_ID");
            DropTable("dbo.Projects");
        }
    }
}
