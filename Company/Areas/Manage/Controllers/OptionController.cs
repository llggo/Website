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
    [Authorize(Roles = "Admin, Mod")]
    public class OptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Option/Edit/5
        public async Task<ActionResult> Edit()
        {
            var option = await db.Option.FirstOrDefaultAsync();

            if (option == null)
            {
                return HttpNotFound();
            }
            var optionlanguage = await (from ol in db.OptionLanguage
                                      where ol.OptionId == option.Id
                                      select ol).ToListAsync();
            var old = new Dictionary<string, OptionLanguage>() { };
            foreach (var m in optionlanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();
                old.Add(langcode.Code, m);
            }

            var slider = (await (from s in db.Slider
                                join sl in db.SliderLanguage on s.Id equals sl.SliderId
                                join l in db.Language on sl.LanguageId equals l.Id
                                where l.Code == language
                                select new
                                {
                                    s = s,
                                    sl = sl,
                                    l = l
                                }).ToListAsync()).Select(t => new CrudSlider
                                {
                                   Slider = t.s,
                                   Language = t.l,
                                   SliderLanguage = new Dictionary<string, SliderLanguage> { { language, t.sl } }
                                }).ToList();

            var client = (await (from c in db.Client
                                 join cl in db.ClientLanguage on c.Id equals cl.ClientId
                                 join l in db.Language on cl.LanguageId equals l.Id
                                 where l.Code == language
                                 select new
                                 {
                                     c = c,
                                     cl = cl,
                                     l = l
                                 }).ToListAsync()).Select(t => new CrudClient
                                 {
                                     Client = t.c,
                                     Language = t.l,
                                     ClientLanguage = new Dictionary<string, ClientLanguage> { { language, t.cl } }
                                 }).ToList();

            var em = new CrudOption()
            {
                Option = option,
                OptionLanguage = old,
                CrudSlider = slider,
                CrudClient = client
            };

            if (option == null)
            {
                return HttpNotFound();
            }

            return View(em);
        }

        // POST: Manage/Option/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Option,Language,OptionLanguage")] CrudOption crudoption)
        {
            if (ModelState.IsValid)
            {
                crudoption.Option.LastEditTime = DateTime.Now;
                crudoption.Option.LastEditUser = User.Identity.GetUserId();
                db.Entry(crudoption.Option).State = EntityState.Modified;

                await db.SaveChangesAsync();

                foreach (var o in crudoption.OptionLanguage)
                {
                    db.Entry(o.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit");
            }
            return View(crudoption);
        }

        // GET: Manage/Option/Delete/5

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
