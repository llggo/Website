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

namespace Company.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, Support, Mod")]
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Slider
        public ActionResult Index()
        {
            return RedirectToAction("Edit", "Option", new { area = "Manage" });
        }

        // GET: Manage/Slider/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = await db.Slider.FindAsync(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Manage/Slider/Create
        public ActionResult Create()
        {
            var model = new CrudSlider();
            return View(model);
        }

        // POST: Manage/Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Slider,SliderLanguage,Language")] CrudSlider cs)
        {
            if (ModelState.IsValid)
            {
                cs.Slider.CreateTime = DateTime.Now;
                cs.Slider.LastEditTime = DateTime.Now;
                cs.Slider.CreateUser = User.Identity.GetUserId();
                db.Slider.Add(cs.Slider);
                await db.SaveChangesAsync();

                foreach (var m in cs.SliderLanguage)
                {
                    m.Value.SliderId = cs.Slider.Id;
                    db.SliderLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit", "Option", new { area = "Manage" });
            }
            return View(cs);
        }

        // GET: Manage/Slider/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var slider = await db.Slider.FindAsync(id);

            if (slider == null)
            {
                return HttpNotFound();
            }


            var sliderlanguage = await (from sl in db.SliderLanguage
                                         where sl.SliderId == slider.Id
                                         select sl).ToListAsync();

            var sld = new Dictionary<string, SliderLanguage>() { };

            foreach (var m in sliderlanguage)
            {
                var langcode = await (from l in db.Language
                                      where l.Id == m.LanguageId
                                      select l).FirstOrDefaultAsync();

                sld.Add(langcode.Code, m);
            }

            var cs = new CrudSlider()
            {
                Slider = slider,
                SliderLanguage = sld
            };

            return View(cs);
        }

        // POST: Manage/Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Slider,SliderLanguage,Language")] CrudSlider cs)
        {
            if (ModelState.IsValid)
            {
                cs.Slider.LastEditTime = DateTime.Now;
                cs.Slider.LastEditUser = User.Identity.GetUserId();
                db.Entry(cs.Slider).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in cs.SliderLanguage)
                {
                    db.Entry(m.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit", "Option", new { area = "Manage" });
            }
            return View(cs);
        }

        // GET: Manage/Slider/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = await db.Slider.FindAsync(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Manage/Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Slider slider = await db.Slider.FindAsync(id);
            db.Slider.Remove(slider);

            List<SliderLanguage> sliderlanguage = await (from sl in db.SliderLanguage
                                                   where sl.SliderId == slider.Id
                                                   select sl).ToListAsync();
            foreach(var sl in sliderlanguage)
            {
                db.SliderLanguage.Remove(sl);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Edit","Option", new { area = "Manage"});
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
