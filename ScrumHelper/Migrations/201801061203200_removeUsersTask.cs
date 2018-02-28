namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeUsersTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Lastname", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Lastname");
        }
    }
}
