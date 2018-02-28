namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "DataAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "DataAdded");
        }
    }
}
