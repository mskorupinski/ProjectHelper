namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSprintTask : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SprintTasks", "Name", c => c.String());
            AlterColumn("dbo.SprintTasks", "Note", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SprintTasks", "Note", c => c.Int(nullable: false));
            AlterColumn("dbo.SprintTasks", "Name", c => c.Int(nullable: false));
        }
    }
}
