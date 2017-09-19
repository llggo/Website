using Company.Areas.Manage.Models;
using Company.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Threading;

namespace Company.Controllers
{
    public class BlogController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;
        // GET: Blog
        public async Task<ActionResult> Index(int? categoryId, int? page)
        {
            var List = new List<ArticleView>();

            if (categoryId == null)
            {
                List = await (from a in db.Article
                              join c in db.Category on a.CategoryId equals c.Id
                              join al in db.ArticleLanguage on a.Id equals al.ArticleId
                              join l in db.Language on al.LanguageId equals l.Id
                              orderby a.LastEditTime descending
                              where l.Code == language && c.Visible == true && c.Enable == true
                              select new ArticleView
                              {
                                  Article = a,
                                  ArticleLanguage = al
                              }).ToListAsync();
            }
            else
            {
                ViewBag.categoryId = categoryId;
                List = await (from a in db.Article
                              join c in db.Category on a.CategoryId equals c.Id
                              join al in db.ArticleLanguage on a.Id equals al.ArticleId
                              join l in db.Language on al.LanguageId equals l.Id
                              orderby a.LastEditTime descending
                              where l.Code == language && c.Id == categoryId && c.Visible == true && c.Enable == true
                              select new ArticleView
                              {
                                  Article = a,
                                  ArticleLanguage = al
                              }).ToListAsync();
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(List.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public async Task<ActionResult> View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = await db.Article.FindAsync(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            var articlelanguage = await (from al in db.ArticleLanguage
                                         join l in db.Language on al.LanguageId equals l.Id
                                         where al.ArticleId == article.Id && l.Code == language
                                         select al).FirstOrDefaultAsync();


            return View(new ArticleView { Article = article,ArticleLanguage = articlelanguage});
        }

        [ChildActionOnly]
        public PartialViewResult Categories()
        {
            var list = (from c in db.Category
                         join cl in db.CategoryLanguage on c.Id equals cl.CategoryId
                         join l in db.Language on cl.LanguageId equals l.Id
                         where l.Code == language && c.Visible == true && c.Enable == true
                         select new CategoryView
                         {
                             Category = c,
                             CategoryLanguage = cl
                         }).ToList();

            return PartialView(list);
        }

        public PartialViewResult RecentPosts()
        {
            var list = (from a in db.Article
                        join c in db.Category on a.CategoryId equals c.Id
                        join al in db.ArticleLanguage on a.Id equals al.ArticleId
                        join l in db.Language on al.LanguageId equals l.Id
                        orderby a.LastEditTime descending
                        where l.Code == language && c.Visible == true && c.Enable == true
                        select new ArticleView
                        {
                            Article = a,
                            ArticleLanguage = al
                        }).Take(5).ToList();

            return PartialView(list);
        }
    }
}