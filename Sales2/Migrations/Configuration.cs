namespace Sales.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sales.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Sales.Models.ApplicationDbContext";
        }

        protected override void Seed(Sales.Models.ApplicationDbContext context)
        {


            context.Menu.AddOrUpdate(
                p => p.Id,
                    new Areas.Manage.Models.MenuModel {Id = 1, Name = "Home", IsRoot = true, IsParent = false, Visible = true, Create = new Models.Modified {Time = DateTime.Now, User = null }, Edit = new Models.Modified { Time = DateTime.Now, User = null } },
                    new Areas.Manage.Models.MenuModel {Id = 2, Name = "Blog", IsRoot = true, IsParent = false, Visible = true, Create = new Models.Modified { Time = DateTime.Now, User = null }, Edit = new Models.Modified { Time = DateTime.Now, User = null } }
                );

            context.Category.AddOrUpdate(
                p => p.Id,
                    new Areas.Manage.Models.Category { Id = 1, Name = "Sản phẩm đặc biệt", Visible = true, Create = new Models.Modified { Time = DateTime.Now, User = null }, Edit = new Models.Modified { Time = DateTime.Now, User = null } },
                    new Areas.Manage.Models.Category { Id = 2, Name = "Sản phẩm", Visible = true, Create = new Models.Modified { Time = DateTime.Now, User = null }, Edit = new Models.Modified { Time = DateTime.Now, User = null } }
                );

            context.Option.AddOrUpdate(
                p => p.Id,
                new Areas.Manage.Models.OptionModel {Id = 1, Title = "Thanghoafoods", Create = new Models.Modified { Time = DateTime.Now, User = null }, Edit = new Models.Modified { Time = DateTime.Now, User = null }}
                );
        }
    }
}
