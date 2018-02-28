namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProjectSqlDateTime2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "DataAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "DataAdded");
        }
    }
}
