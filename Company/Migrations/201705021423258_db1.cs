namespace Company.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.OptionLanguages", "SliderTitle");
            DropColumn("dbo.OptionLanguages", "SliderDescribe");
            DropColumn("dbo.OptionLanguages", "SliderTarget");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OptionLanguages", "SliderTarget", c => c.String());
            AddColumn("dbo.OptionLanguages", "SliderDescribe", c => c.String());
            AddColumn("dbo.OptionLanguages", "SliderTitle", c => c.String());
            DropTable("dbo.ProductImages");
        }
    }
}
