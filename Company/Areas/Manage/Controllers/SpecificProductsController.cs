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
    public class SpecificProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Article
        public async Task<ActionResult> Index()
        {
            var SProducts = (await (from a in db.SProduct
                               join al in db.SProductLanguage on a.Id equals al.SProductId
                               join l in db.Language on al.LanguageId equals l.Id
                               where l.Code == language
                               select new
                               {
                                   a = a,
                                   al = al,
                                   l = l
                               }).ToListAsync()).Select(t => new CrudSProduct
                               {
                                   SProduct = t.a,
                                   Language = t.l,
                                   SProductLanguage = new Dictionary<string, SProductLanguage> { { language, t.al } }
                               });

            return View(SProducts);
        }

        // GET: Manage/Article/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SProduct SProducts = await db.SProduct.FindAsync(id);
            if (SProducts == null)
            {
                return HttpNotFound();
            }
            return View(SProducts);
        }

        // GET: Manage/Article/Create
        public ActionResult Create()
        {
            var model = new CrudSProduct();
            return View(model);
        }

        // POST: Manage/Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SProduct,SProductLanguage,Language")] CrudSProduct cp)
        {
            if (ModelState.IsValid)
            {
                cp.SProduct.CreateTime = cp.SProduct.LastEditTime = DateTime.Now;
                cp.SProduct.CreateUser = cp.SProduct.LastEditUser = User.Identity.GetUserId();
                db.SProduct.Add(cp.SProduct);
                await db.SaveChangesAsync();

                foreach (var m in cp.SProductLanguage)
                {
                    m.Value.SProductId = cp.SProduct.Id;
                    db.SProductLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(cp);
        }

        // GET: Manage/Article/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var SProduct = await db.SProduct.FindAsync(id);

            if (SProduct == null)
            {
                return HttpNotFound();
            }
            var SProductlanguage = await (from ol in db.SProductLanguage
                                         where ol.SProductId == SProduct.Id
                                         select ol).ToListAsync();
            var old = new Dictionary<string, SProductLanguage>() { };
            foreach (var m in SProductlanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();
                old.Add(langcode.Code, m);
            }


            var em = new CrudSProduct()
            {
                SProduct = SProduct,
                SProductLanguage = old
            };


            return View(em);
        }

        // POST: Manage/Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SProduct,SProductLanguage,Language")] CrudSProduct CrudSProduct)
        {
            if (ModelState.IsValid)
            {
                CrudSProduct.SProduct.LastEditTime = DateTime.Now;
                CrudSProduct.SProduct.LastEditUser = User.Identity.GetUserId();
                db.Entry(CrudSProduct.SProduct).State = EntityState.Modified;

                await db.SaveChangesAsync();

                foreach (var o in CrudSProduct.SProductLanguage)
                {
                    db.Entry(o.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit");
            }
            return View(CrudSProduct);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SProduct SProduct = await db.SProduct.FindAsync(id);
            if (SProduct == null)
            {
                return HttpNotFound();
            }
            return View(SProduct);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SProduct SProduct = await db.SProduct.FindAsync(id);
            db.SProduct.Remove(SProduct);

            var pl = await (from al in db.SProductLanguage
                                         where al.SProductId == SProduct.Id
                                         select al).ToListAsync();

            foreach (var al in pl)
            {
                db.SProductLanguage.Remove(al);
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
