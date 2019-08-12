namespace Sales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Language", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Language");
        }
    }
}
