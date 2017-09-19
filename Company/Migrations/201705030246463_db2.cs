namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OptionLanguages", "LogoProducts", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OptionLanguages", "LogoProducts");
        }
    }
}
