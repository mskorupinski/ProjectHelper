namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProjectSqlDateTime : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Projects", "DataAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "DataAdded", c => c.DateTime(nullable: false));
        }
    }
}
