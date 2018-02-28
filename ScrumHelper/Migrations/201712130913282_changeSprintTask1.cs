namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSprintTask1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SprintTasks", "DataAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SprintTasks", "DataAdded");
        }
    }
}
