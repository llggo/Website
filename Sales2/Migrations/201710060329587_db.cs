namespace Sales.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Describle = c.String(),
                        Content = c.String(),
                        Order = c.Int(),
                        Visible = c.Boolean(nullable: false),
                        Edit_Time = c.DateTime(),
                        Edit_User = c.String(),
                        Create_Time = c.DateTime(),
                        Create_User = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Articles");
        }
    }
}
