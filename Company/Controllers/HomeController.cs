using Company.Areas.Manage.Models;
using Company.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Company.Extends.Library;
using System;
using System.IO;

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
                                    }).Take(5).ToListAsync(),

                ClientView = (await (from c in db.Client
                                     join cl in db.ClientLanguage on c.Id equals cl.ClientId
                                     join l in db.Language on cl.LanguageId equals l.Id
                                     where l.Code == language
                                     orderby c.Order
                                     select new ClientView
                                     {
                                         Client = c,
                                         ClientLanguage = cl
                                     }).ToListAsync()).ToList(),

                OptionView = data.OptionView(),



                SpecificProductsView = await (from a in db.Article
                                              join c in db.Category on a.CategoryId equals c.Id
                                              join al in db.ArticleLanguage on a.Id equals al.ArticleId
                                              join l in db.Language on al.LanguageId equals l.Id
                                              where c.Id == 3 && l.Code == language
                                              select new ArticleView
                                              {
                                                  Article = a,
                                                  ArticleLanguage = al
                                              }).Take(5).ToListAsync(),

                ArticleViewComments = await (from a in db.Article
                                             join c in db.Category on a.CategoryId equals c.Id
                                             join al in db.ArticleLanguage on a.Id equals al.ArticleId
                                             join l in db.Language on al.LanguageId equals l.Id
                                             where c.Id == 2 && l.Code == language
                                             select new ArticleView
                                             {
                                                 Article = a,
                                                 ArticleLanguage = al
                                             }).ToListAsync()
            };
            return View(hv);
        }

        public async Task<ActionResult> Products()
        {
            var data = new Data();
            ProductsView pv = new ProductsView();

            var product = await (from prd in db.Product
                     join pl in db.ProductLanguage on prd.Id equals pl.ProductId
                     join l in db.Language on pl.LanguageId equals l.Id
                     where l.Code == language
                     select new Products
                     {
                         Product = prd,
                         ProducLanguage = pl
                     }).ToListAsync();

            foreach(var p in product)
            {
                p.ProductImage = await (from pi in db.ProductImage
                                  where pi.ProductId == p.Product.Id
                                  select pi).ToListAsync();
            }


            pv.Products = product;
            pv.Logo = data.OptionView().OptionLanguage.LogoProducts;
            return View(pv);
        }

        public async Task<ActionResult> ListSpecificProducts()
        {
            var List = new List<SpecificProductsView>();
            
            List = await (from a in db.SProduct
                            join al in db.SProductLanguage on a.Id equals al.SProductId
                            join l in db.Language on al.LanguageId equals l.Id
                            where l.Code == language
                            select new SpecificProductsView
                            {
                                SProduct = a,
                                SProductLanguage = al
                            }).ToListAsync();
            


            return View(List);
        }

        public async Task<ActionResult> SpecificProducts(int id)
        {
            var spv = await (from a in db.SProduct
                             join al in db.SProductLanguage on a.Id equals al.SProductId
                             join l in db.Language on al.LanguageId equals l.Id
                             where l.Code == language && a.Id == id
                             select new SpecificProductsView
                             {
                                 SProduct = a,
                                 SProductLanguage = al
                             }).FirstOrDefaultAsync();

            return View(spv);
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

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
            string[] mod = { "mod@example.com" };
            string[] support = { "support@example.com" };

            const string password = "Admin@123456";

            string[] roleName = { "Admin", "Mod", "Support" };

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

            foreach (var m in mod)
            {
                var user = userManager.FindByName(m);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = m, Email = m };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(roleName[1]))
                {
                    var result = userManager.AddToRole(user.Id, roleName[1]);
                }
            }

            foreach (var s in support)
            {
                var user = userManager.FindByName(s);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = s, Email = s };
                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(roleName[2]))
                {
                    var result = userManager.AddToRole(user.Id, roleName[2]);
                }
            }

            return null;
        }

        //public ActionResult Backup()
        //{
        //    var l = db.Language.ToList();
        //    var ll = db.LanguageLanguage.ToList();

        //    ViewBag.Language = l;
        //    ViewBag.LanguageLanguage = ll;

        //    var lll = db.LanguageLanguage.Find(4);
        //    lll.Name = "Tiếng Việt";

        //    var entry = db.Entry(lll);
        //    db.SaveChanges();

        //    return View();
        //}
    }
}
