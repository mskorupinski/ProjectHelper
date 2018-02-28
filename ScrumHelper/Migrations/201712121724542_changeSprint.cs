namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSprint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sprints", "DateAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sprints", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
