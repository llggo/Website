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
using Microsoft.AspNet.Identity;
using System.Threading;

namespace Company.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, Support, Mod")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Categories
        public async Task<ActionResult> Index()
        {
            var category = (await (from c in db.Category
                               join cl in db.CategoryLanguage on c.Id equals cl.CategoryId
                               join l in db.Language on cl.LanguageId equals l.Id
                               orderby c.Order ascending
                               where l.Code == language
                               select new
                               {
                                   c = c,
                                   cl = cl,
                                   l = l
                               }).ToListAsync()).Select(t => new CrudCategory
                               {
                                   Category = t.c,
                                   Language = t.l,
                                   CategoryLanguage = new Dictionary<string, CategoryLanguage> { { language, t.cl } }
                               });

            return View(category);
        }

        // GET: Manage/Categories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Category.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Manage/Categories/Create
        public ActionResult Create()
        {
            CrudCategory ec = new CrudCategory();
            return View(ec);
        }

        // POST: Manage/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Category,CategoryLanguage,Language")] CrudCategory ec)
        {
            if (ModelState.IsValid)
            {
                ec.Category.CreateTime = ec.Category.LastEditTime = DateTime.Now;
                ec.Category.CreateUser = ec.Category.LastEditUser = User.Identity.GetUserId();
                db.Category.Add(ec.Category);
                await db.SaveChangesAsync();

                foreach (var m in ec.CategoryLanguage)
                {
                    m.Value.CategoryId = ec.Category.Id;
                    db.CategoryLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(ec);
        }

        // GET: Manage/Categories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = await db.Category.FindAsync(id);

            if (category == null)
            {
                return HttpNotFound();
            }


            var categorylanguage = await (from cl in db.CategoryLanguage
                                      where cl.CategoryId == category.Id
                                      select cl).ToListAsync();

            var cld = new Dictionary<string, CategoryLanguage>() { };

            foreach (var m in categorylanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();

                cld.Add(langcode.Code, m);
            }

            var ec = new CrudCategory()
            {
                Category = category,
                CategoryLanguage = cld
            };

            
            return View(ec);
        }

        // POST: Manage/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Category,CategoryLanguage,Language")] CrudCategory ec)
        {
            if (ModelState.IsValid)
            {
                ec.Category.LastEditTime = DateTime.Now;
                ec.Category.LastEditUser = User.Identity.GetUserId();
                db.Entry(ec.Category).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in ec.CategoryLanguage)
                {
                    
                    db.Entry(m.Value).State = EntityState.Modified;
                }

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ec);
        }

        // GET: Manage/Categories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Category.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Manage/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Category category = await db.Category.FindAsync(id);
            db.Category.Remove(category);

            var categorylanguage = await (from cl in db.CategoryLanguage
                                         where cl.CategoryId == category.Id
                                         select cl).ToListAsync();

            foreach (var cl in categorylanguage)
            {
                db.CategoryLanguage.Remove(cl);
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
    }
}
