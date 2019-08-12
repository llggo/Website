using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sales.Areas.Manage.Models;
using Sales.Models;
using Microsoft.AspNet.Identity;

namespace Sales.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Article
        public async Task<ActionResult> Index()
        {
            return View(await db.Article.ToListAsync());
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
            var model = new Article();
            model.Visible = true;

            return View(model);
        }

        // POST: Manage/Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Image,Describle,Content,Visible")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Create = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };
                article.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Article.Add(article);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Manage/Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Manage/Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Image,Describle,Content,Visible,Create")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Entry(article).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(article);
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
    }
}
