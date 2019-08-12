using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Controllers
{
    public class ArticleController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Article
        public ActionResult Index()
        {
            return View(db.Article.ToList());
        }

        public ActionResult Details(int? id  = null)
        {
            var article = from a in db.Article
                          where a.Id == id
                          select a;

            return View(article.FirstOrDefault());
        }
    }
}