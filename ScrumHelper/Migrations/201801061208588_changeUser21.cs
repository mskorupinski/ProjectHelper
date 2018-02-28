namespace ScrumHelper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUser21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Mail", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Mail");
        }
    }
}
