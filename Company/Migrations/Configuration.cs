namespace Company.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Company.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Company.Models.ApplicationDbContext";
        }

        protected override void Seed(Company.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Language.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Language {Id = 1, Code = "en-US" }, //id 1
                    new Areas.Manage.Models.Language {Id = 2, Code = "vi" } //id 2
                );

            context.LanguageLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.LanguageLanguage { Id = 1, LanguageObjectId = 1, LanguageCheckId = 1, Name = "English" },
                    new Areas.Manage.Models.LanguageLanguage { Id = 2, LanguageObjectId = 1, LanguageCheckId = 2, Name = "Tiếng anh" },
                    new Areas.Manage.Models.LanguageLanguage { Id = 3, LanguageObjectId = 2, LanguageCheckId = 1, Name = "Vietnamese" },
                    new Areas.Manage.Models.LanguageLanguage { Id = 4, LanguageObjectId = 2, LanguageCheckId = 2, Name = "Việt nam" }

                );

            context.Menu.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Menu { Id = 1, CreateTime = DateTime.Now, LastEditTime = DateTime.Now, Visible = true, Target = "/home/products" }
                );

            context.MenuLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.MenuLanguage { Id = 1, MenuId = 1, LanguageId = 1, Name = "Products"},
                    new Areas.Manage.Models.MenuLanguage { Id = 2, MenuId = 1, LanguageId = 2, Name = "Sản phẩm" }

                );

            context.Category.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Category { Id = 1, CreateTime = DateTime.Now, LastEditTime = DateTime.Now, Visible = true, Enable = true },
                    new Areas.Manage.Models.Category { Id = 2, CreateTime = DateTime.Now, LastEditTime = DateTime.Now, Visible = false, Enable = true },
                    new Areas.Manage.Models.Category { Id = 3, CreateTime = DateTime.Now, LastEditTime = DateTime.Now, Visible = false, Enable = true }
                );

            context.CategoryLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.CategoryLanguage { Id = 1, CategoryId = 3, LanguageId = 1, Name = "Our solution" },
                    new Areas.Manage.Models.CategoryLanguage { Id = 2, CategoryId = 3, LanguageId = 2, Name = "Giải pháp của chúng tôi" },
                    new Areas.Manage.Models.CategoryLanguage { Id = 3, CategoryId = 2, LanguageId = 1, Name = "Why us" },
                    new Areas.Manage.Models.CategoryLanguage { Id = 4, CategoryId = 2, LanguageId = 2, Name = "Tại sao là chúng tôi" },
                    new Areas.Manage.Models.CategoryLanguage { Id = 5, CategoryId = 1, LanguageId = 1, Name = "Slider" },
                    new Areas.Manage.Models.CategoryLanguage { Id = 6, CategoryId = 1, LanguageId = 2, Name = "Trình chiếu" }
                );

            context.Article.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Article { Id = 1, CategoryId = 1, Enable = true, Visible = true, CreateTime = DateTime.Now, LastEditTime = DateTime.Now },
                    new Areas.Manage.Models.Article { Id = 2, CategoryId = 2, Enable = true, Visible = true, CreateTime = DateTime.Now, LastEditTime = DateTime.Now },
                    new Areas.Manage.Models.Article { Id = 3, CategoryId = 3, Enable = true, Visible = true, CreateTime = DateTime.Now, LastEditTime = DateTime.Now }
                );

            context.ArticleLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.ArticleLanguage { Id = 1, ArticleId = 1, LanguageId = 1, Name = "Slider 1" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 2, ArticleId = 1, LanguageId = 2, Name = "Trình chiếu 1" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 3, ArticleId = 2, LanguageId = 1, Name = "Cricle 1", Image = "100", Describe = "blue"},
                    new Areas.Manage.Models.ArticleLanguage { Id = 4, ArticleId = 2, LanguageId = 2, Name = "Vòng tròn 1", Image = "100", Describe = "blue" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 5, ArticleId = 2, LanguageId = 1, Name = "Cricle 2", Image = "60", Describe = "red" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 6, ArticleId = 2, LanguageId = 2, Name = "Vòng tròn 2", Image = "60", Describe = "red" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 5, ArticleId = 2, LanguageId = 1, Name = "Cricle 3", Image = "40", Describe = "green" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 6, ArticleId = 2, LanguageId = 2, Name = "Vòng tròn 3", Image = "40", Describe = "green" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 7, ArticleId = 3, LanguageId = 1, Name = "Special Solution 1" },
                    new Areas.Manage.Models.ArticleLanguage { Id = 8, ArticleId = 3, LanguageId = 2, Name = "Sản phẩm thực tế 1" }
                );



            context.Slider.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Slider { Id = 1, Visible = true, CreateTime = DateTime.Now, LastEditTime = DateTime.Now },
                    new Areas.Manage.Models.Slider { Id = 2, Visible = true, CreateTime = DateTime.Now, LastEditTime = DateTime.Now }
                );

            context.SliderLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.SliderLanguage { Id = 1, SliderId = 1, LanguageId = 1, Name = "Hot" },
                    new Areas.Manage.Models.SliderLanguage { Id = 2, SliderId = 1, LanguageId = 2, Name = "Nóng" },
                    new Areas.Manage.Models.SliderLanguage { Id = 3, SliderId = 2, LanguageId = 1, Name = "Cool" },
                    new Areas.Manage.Models.SliderLanguage { Id = 4, SliderId = 2, LanguageId = 2, Name = "Lạnh" }
                );

            context.Option.AddOrUpdate(
                    p => p.Id,
                        new Areas.Manage.Models.Option
                        {
                            Id = 1,
                            ClientEnable = true,
                            OurProductsEnable = true,
                            LiveChatEnable = true,
                            LastEditTime = DateTime.Now,
                        }
                    );

            context.OptionLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.OptionLanguage
                    {
                        Id = 1,
                        OptionId = 1,
                        LanguageId = 1,
                        LogoPath = "~/Images/logo.png",
                        WhyUs = "Why us oh!",
                        AboutUs = "About us oh!",
                    },
                    new Areas.Manage.Models.OptionLanguage
                    {
                        Id = 2,
                        OptionId = 1,
                        LanguageId = 2,
                        LogoPath = "~/Images/logo.png",
                        WhyUs = "beep ****",
                        AboutUs = "Abeep ****",
                    }
                );

            context.SProduct.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.SProduct
                    {
                        Id = 1,
                        LastEditTime = DateTime.Now
                    }
                );

            context.SProductLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.SProductLanguage
                    {
                        Id = 1,
                        SProductId = 1,
                        LanguageId = 1,
                        Describe = "Describe"
                    },
                    new Areas.Manage.Models.SProductLanguage
                    {
                        Id = 2,
                        SProductId = 1,
                        LanguageId = 2,
                        Describe = "Rút gọn"
                    }
                );


            context.Product.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.Product
                    {
                        Id = 1,
                        LastEditTime = DateTime.Now
                    }
                );

            context.ProductLanguage.AddOrUpdate(
                    p => p.Id,
                    new Areas.Manage.Models.ProductLanguage
                    {
                        Id = 1,
                        ProductId = 1,
                        LanguageId = 1,

                    },
                    new Areas.Manage.Models.ProductLanguage
                    {
                        Id = 2,
                        ProductId = 1,
                        LanguageId = 2,
                    }
                );
        }
    }
}
