using Company.Extends.Library;
using Company.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;
        public async Task<ActionResult> Index()
        {
            var data = new Data();
            HomeView hv = new HomeView()
            {
                SliderView = await (from a in db.Article
                                    join c in db.Category on a.CategoryId equals c.Id
                                    join al in db.ArticleLanguage on a.Id equals al.ArticleId
                                    join l in db.Language on al.LanguageId equals l.Id
                                    where c.Id == 1 && l.Code == language
                                    select new ArticleView
                                    {
                                        Article = a,
                                        ArticleLanguage = al
                                    }).Take(5).ToListAsync()
            };
            return View(hv);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Init()
        {
            var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

            string[] admin = { "admin@example.com" };

            const string password = "Admin@123456";

            string[] roleName = { "Admin"};

            foreach (var r in roleName)
            {
                //Create Role Admin if it does not exist
                var role = roleManager.FindByName(r);
                if (role == null)
                {
                    role = new IdentityRole(r);
                    var roleresult = roleManager.Create(role);
                }

            }

            foreach (var a in admin)
            {
                var user = userManager.FindByName(a);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = a, Email = a };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(roleName[0]))
                {
                    var result = userManager.AddToRole(user.Id, roleName[0]);
                }
            }

            

            return null;
        }
    }
}