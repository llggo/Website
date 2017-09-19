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

namespace Company.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LanguageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Language
        public async Task<ActionResult> Index()
        {
            var lang = (await (from ll in db.LanguageLanguage
                                join l in db.Language on ll.LanguageObjectId equals l.Id
                                join lt in db.Language on ll.LanguageCheckId equals lt.Id
                                where lt.Code == language
                                select new
                                {
                                    ll = ll,
                                    l = l,
                                }).ToListAsync()).Select(t => new CrudLanguage
                                {
                                    Language = t.l,
                                     LanguageLanguage= new Dictionary<string, LanguageLanguage> { { language, t.ll } }
                                });

            return View(lang);
        }

        // GET: Manage/Language/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = await db.Language.FindAsync(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // GET: Manage/Language/Create
        public ActionResult Create()
        {
            var model = new CrudLanguage();
            return View(model);
        }

        // POST: Manage/Language/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Language, LanguageLanguage")] CrudLanguage cl)
        {
            if (ModelState.IsValid)
            {
                
                db.Language.Add(cl.Language);
                await db.SaveChangesAsync();

                foreach (var m in cl.LanguageLanguage)
                {
                    m.Value.LanguageObjectId = cl.Language.Id;
                    db.LanguageLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: Manage/Language/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lang = await db.Language.FindAsync(id);

            if (lang == null)
            {
                return HttpNotFound();
            }

            var languagelanguage = await (from ll in db.LanguageLanguage
                                      where ll.LanguageCheckId == lang.Id
                                      select ll).ToListAsync();

            var lld = new Dictionary<string, LanguageLanguage>() { };

            foreach (var m in languagelanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageCheckId
                                      select lc).FirstOrDefaultAsync();

                lld.Add(langcode.Code, m);
            }

            var cl = new CrudLanguage()
            {
                Language = lang,
                LanguageLanguage = lld
            };

            return View(cl);
        }

        // POST: Manage/Language/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Language,LanguageLanguage")] CrudLanguage cl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cl.Language).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in cl.LanguageLanguage)
                {
                    db.Entry(m.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: Manage/Language/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Language language = await db.Language.FindAsync(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }

        // POST: Manage/Language/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Language language = await db.Language.FindAsync(id);
            db.Language.Remove(language);

            var languagelanguage = await (from ll in db.LanguageLanguage
                                          where ll.LanguageObjectId == language.Id
                                          select ll).ToListAsync();

            foreach (var ll in languagelanguage)
            {
                db.LanguageLanguage.Remove(ll);
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
