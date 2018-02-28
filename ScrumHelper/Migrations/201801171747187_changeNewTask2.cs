namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeNewTask2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Name", c => c.String());
            AlterColumn("dbo.Projects", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Notes", c => c.String(nullable: false));
            AlterColumn("dbo.Projects", "Name", c => c.String(nullable: false));
        }
    }
}
