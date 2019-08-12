namespace Company.Migrations
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
                        Visible = c.Boolean(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Describe = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Enable = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.String(),
                        Visible = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Infomation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Message = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Info = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguageLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageObjectId = c.Int(nullable: false),
                        LanguageCheckId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        isDropdown = c.Boolean(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        Order = c.Int(),
                        Visible = c.Boolean(nullable: false),
                        Target = c.String(),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OptionId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        LogoPath = c.String(),
                        LogoBottomPath = c.String(),
                        AboutUs = c.String(),
                        AboutUsTarget = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.String(),
                        Visible = c.Boolean(nullable: false),
                        FloatRight = c.Boolean(nullable: false),
                        CreateUser = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        LastEditUser = c.String(),
                        LastEditTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SliderLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SliderId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Content = c.String(),
                        Infomation = c.String(),
                        Target = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.UserProfiles", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.UserProfiles");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.SliderLanguages");
            DropTable("dbo.Sliders");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.OptionLanguages");
            DropTable("dbo.Options");
            DropTable("dbo.MenuLanguages");
            DropTable("dbo.Menus");
            DropTable("dbo.LanguageLanguages");
            DropTable("dbo.Languages");
            DropTable("dbo.Customers");
            DropTable("dbo.ClientLanguages");
            DropTable("dbo.Clients");
            DropTable("dbo.CategoryLanguages");
            DropTable("dbo.Categories");
            DropTable("dbo.ArticleLanguages");
            DropTable("dbo.Articles");
        }
    }
}
