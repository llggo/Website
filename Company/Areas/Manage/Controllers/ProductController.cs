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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Categories
        public async Task<ActionResult> Index()
        {
            var category = (await (from c in db.Product
                               join cl in db.ProductLanguage on c.Id equals cl.ProductId
                               join l in db.Language on cl.LanguageId equals l.Id
                               where l.Code == language
                               select new
                               {
                                   c = c,
                                   cl = cl,
                                   l = l
                               }).ToListAsync()).Select(t => new CrudProduct
                               {
                                   Product = t.c,
                                   Language = t.l,
                                   ProductLanguage = new Dictionary<string, ProductLanguage> { { language, t.cl } }
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
            Product category = await db.Product.FindAsync(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Manage/Categories/Create
        public ActionResult Create()
        {
            CrudProduct ec = new CrudProduct();
            return View(ec);
        }

        // POST: Manage/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Product,ProductLanguage,Language")] CrudProduct ec)
        {
            if (ModelState.IsValid)
            {
                ec.Product = new Product();
                ec.Product.LastEditTime = DateTime.Now;
                ec.Product.LastEditUser = User.Identity.GetUserId();
                db.Product.Add(ec.Product);
                await db.SaveChangesAsync();

                foreach (var m in ec.ProductLanguage)
                {
                    m.Value.ProductId = ec.Product.Id;
                    db.ProductLanguage.Add(m.Value);
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

            var category = await db.Product.FindAsync(id);

            if (category == null)
            {
                return HttpNotFound();
            }


            var categorylanguage = await (from cl in db.ProductLanguage
                                          where cl.ProductId == category.Id
                                      select cl).ToListAsync();

            var cld = new Dictionary<string, ProductLanguage>() { };

            foreach (var m in categorylanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();

                cld.Add(langcode.Code, m);
            }

            var ec = new CrudProduct()
            {
                Product = category,
                ProductLanguage = cld
            };

            
            return View(ec);
        }

        // POST: Manage/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Product,ProductLanguage,Language")] CrudProduct ec)
        {
            if (ModelState.IsValid)
            {
                ec.Product.LastEditTime = DateTime.Now;
                ec.Product.LastEditUser = User.Identity.GetUserId();
                db.Entry(ec.Product).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in ec.ProductLanguage)
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
            Product category = await db.Product.FindAsync(id);
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
            Product category = await db.Product.FindAsync(id);
            db.Product.Remove(category);

            var categorylanguage = await (from cl in db.ProductLanguage
                                          where cl.ProductId == category.Id
                                         select cl).ToListAsync();

            foreach (var cl in categorylanguage)
            {
                db.ProductLanguage.Remove(cl);
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
