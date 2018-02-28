namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSprint2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sprints", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sprints", "EndDate");
        }
    }
}
