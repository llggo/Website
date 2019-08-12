using Sales.Areas.Manage.Models;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using PagedList;
using System.Web.Mvc;

namespace Sales.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index(int? CategoryId, int? page)
        {
            var products = new List<ProducImageView>();

            if (CategoryId == null)
            {
                products = (from p in db.Product
                            orderby p.Id descending
                            select new ProducImageView
                            {
                                Product = p,
                                Image = (from i in db.ProductImage
                                         where i.ProductId == p.Id
                                         select i).FirstOrDefault()
                            }).ToList();
            }
            else
            {
                products = (from p in db.Product
                            orderby p.Id descending
                            where p.CategoryId == CategoryId
                            select new
                            ProducImageView
                            {
                                Product = p,
                                Image = (from i in db.ProductImage
                                         where i.ProductId == p.Id
                                         select i).FirstOrDefault()
                            }).ToList();
            }

            var pageSize = 6;

            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await db.Product.FindAsync(id));
        }
    }
}