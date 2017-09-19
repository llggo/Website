using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Company.Areas.Manage.Models;
using Company.Models;
using System.Threading;
using Microsoft.AspNet.Identity;

namespace Company.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, Support, Mod")]
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Article
        public async Task<ActionResult> Index()
        {
            var article = (await (from a in db.Article
                               join al in db.ArticleLanguage on a.Id equals al.ArticleId
                               join l in db.Language on al.LanguageId equals l.Id
                               where l.Code == language
                               select new
                               {
                                   a = a,
                                   al = al,
                                   l = l
                               }).ToListAsync()).Select(t => new CrudArticle
                               {
                                   Article = t.a,
                                   Language = t.l,
                                   ArticleLanguage = new Dictionary<string, ArticleLanguage> { { language, t.al } }
                               });

            return View(article);
        }

        // GET: Manage/Article/Details/5
        public async Task<ActionResult> Details(int? id)
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
            return View(article);
        }

        // GET: Manage/Article/Create
        public ActionResult Create()
        {
            ViewBag.CategoryList = CategoryList();
            var model = new CrudArticle();
            return View(model);
        }

        // POST: Manage/Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Article,ArticleLanguage,Language")] CrudArticle ea)
        {
            if (ModelState.IsValid)
            {
                ea.Article.CreateTime = ea.Article.LastEditTime = DateTime.Now;
                ea.Article.CreateUser = ea.Article.LastEditUser = User.Identity.GetUserId();
                db.Article.Add(ea.Article);
                await db.SaveChangesAsync();

                foreach (var m in ea.ArticleLanguage)
                {
                    m.Value.ArticleId = ea.Article.Id;
                    db.ArticleLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(ea);
        }

        // GET: Manage/Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = await db.Article.FindAsync(id);

            if (article == null)
            {
                return HttpNotFound();
            }


            var articlelanguage = await (from al in db.ArticleLanguage
                                      where al.ArticleId == article.Id
                                      select al).ToListAsync();

            var ald = new Dictionary<string, ArticleLanguage>() { };

            foreach (var m in articlelanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();

                ald.Add(langcode.Code, m);
            }

            var em = new CrudArticle()
            {
                Article = article,
                ArticleLanguage = ald
            };


            ViewBag.CategoryList = CategoryList();

            return View(em);
        }

        // POST: Manage/Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Article,ArticleLanguage,Language")] CrudArticle ea)
        {
            if (ModelState.IsValid)
            {
                ea.Article.LastEditTime = DateTime.Now;
                ea.Article.LastEditUser = User.Identity.GetUserId();
                db.Entry(ea.Article).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in ea.ArticleLanguage)
                {
                    db.Entry(m.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(ea);
        }

        // GET: Manage/Article/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(article);
        }

        // POST: Manage/Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Article article = await db.Article.FindAsync(id);
            db.Article.Remove(article);

            var articlelanguage = await (from al in db.ArticleLanguage
                                         where al.ArticleId == article.Id
                                         select al).ToListAsync();

            foreach (var al in articlelanguage)
            {
                db.ArticleLanguage.Remove(al);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public List<SelectListItem> CategoryList()
        {
            var category = from c in db.Category
                           join cl in db.CategoryLanguage on c.Id equals cl.CategoryId
                           join l in db.Language on cl.LanguageId equals l.Id
                           where l.Code == language && c.Enable == true
                           select new SelectListItem
                           {
                               Text = cl.Name,
                               Value = c.Id.ToString()
                           };
            List<SelectListItem> item = new List<SelectListItem>();
            item.AddRange(category);
            return item;
        }
    }
}
