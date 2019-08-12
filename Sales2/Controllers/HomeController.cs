using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sales.Areas.Manage.Models;

namespace Sales.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var hv = new HomeView();

            hv.Slider = (from s in db.Slider
                         where s.Visible == true
                         orderby s.Id descending
                         select s).ToList();

            hv.FeaturedProducts = (from p in db.Product
                                   where p.CategoryId == 1
                                   orderby p.Id descending
                                   select new ProducImageView {
                                       Product = p,
                                       Image = (from i in db.ProductImage
                                               where i.ProductId == p.Id
                                               select i).FirstOrDefault()

                                   }).Take(10).ToList();

            hv.LastestProducts = (from p in db.Product
                                  orderby p.Id descending
                                  select new ProducImageView
                                  {
                                      Product = p,
                                      Image = (from i in db.ProductImage
                                               where i.ProductId == p.Id
                                               select i).FirstOrDefault()

                                  }).Take(8).ToList();

            hv.LastestArticle = (from a in db.Article
                                 orderby a.Id descending
                                 select a).Take(4).ToList();

            

            return View(hv);
        }
    }
}