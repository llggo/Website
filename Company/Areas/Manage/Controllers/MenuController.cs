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
using Microsoft.AspNet.Identity.Owin;
using System.Threading;

namespace Company.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin, Mod")]
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        public ActionResult GetParent(int? ParentId)
        {
            if(ParentId == null)
            {
                return Content("Null");
            }

            var Parent = (from m in db.Menu
                       join ml in db.MenuLanguage on m.Id equals ml.MenuId
                       join l in db.Language on ml.LanguageId equals l.Id
                       orderby m.Order ascending
                       where (l.Code == language && m.ParentId == ParentId)
                       select new MenuView
                       {
                           Id = m.Id,
                           Name = ml.Name,
                           isDropdown = m.isDropdown,
                           isActive = m.isActive,
                           Target = m.Target

                       }).FirstOrDefault();

            if (Parent != null)
            {
                return Content(Parent.Name);
            }
            else
            {
                return Content("Null");
            }
        }

        // GET: Manage/Menu
        public async Task<ActionResult> Index()
        {
            var menu = (await (from m in db.Menu
                        join ml in db.MenuLanguage on m.Id equals ml.MenuId
                        join l in db.Language on ml.LanguageId equals l.Id
                        orderby m.Order ascending
                        where l.Code == language
                        select new
                        {
                            m = m,
                            ml = ml,
                            l = l
                        }).ToListAsync()).Select(t => new CrudMenu
                        {
                            Menu = t.m,
                            Language = t.l,
                            MenuLanguage = new Dictionary<string, MenuLanguage> { { language, t.ml } }
                        });

            return View(menu);
        }

        // GET: Manage/Menu/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Manage/Menu/Create
        public ActionResult Create()
        {
            ViewBag.ParentList = ParentList(null);
            var model = new CrudMenu();

            return View(model);
        }

        // POST: Manage/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Menu,Language,MenuLanguage")] CrudMenu em)
        {
            if (ModelState.IsValid)
            {
                em.Menu.CreateTime = DateTime.Now;
                em.Menu.LastEditTime = DateTime.Now;
                em.Menu.CreateUser = User.Identity.GetUserId();
                db.Menu.Add(em.Menu);
                await db.SaveChangesAsync();

                foreach(var m in em.MenuLanguage)
                {
                    m.Value.MenuId = em.Menu.Id;
                    db.MenuLanguage.Add(m.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(em);
        }

        // GET: Manage/Menu/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var menu = await db.Menu.FindAsync(id);

            if (menu == null)
            {
                return HttpNotFound();
            }

            var menulanguage = await (from ml in db.MenuLanguage
                            where ml.MenuId == menu.Id
                            select ml).ToListAsync();

            var mld = new Dictionary<string, MenuLanguage>() { };

            foreach(var m in menulanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();

                mld.Add(langcode.Code, m);
            }

            var em = new CrudMenu()
            {
                Menu = menu,
                MenuLanguage = mld
            };

            

            


            ViewBag.ParentList = ParentList(id);


            List<SelectListItem> item = new List<SelectListItem>();

            item.Add(new SelectListItem { Text = "True", Value = "True" });
            item.Add(new SelectListItem { Text = "False", Value = "False" });
            

            ViewBag.isEnable = item;

            return View(em);
        }

        // POST: Manage/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Menu,Language,MenuLanguage")] CrudMenu em)
        {
            if (ModelState.IsValid)
            {
                em.Menu.LastEditTime = DateTime.Now;
                em.Menu.CreateUser = User.Identity.GetUserId();
                db.Entry(em.Menu).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var m in em.MenuLanguage)
                {
                    db.Entry(m.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(em);
        }

        // GET: Manage/Menu/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = await db.Menu.FindAsync(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Manage/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Menu menu = await db.Menu.FindAsync(id);
            db.Menu.Remove(menu);

            var menulanguage = await (from ml in db.MenuLanguage
                                      where ml.MenuId == menu.Id
                                      select ml).ToListAsync();

            foreach(var ml in menulanguage)
            {
                db.MenuLanguage.Remove(ml);
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

        public List<SelectListItem> ParentList(int? Id)
        {
            List<SelectListItem> ParentList = new List<SelectListItem>();
            var items = (from m in db.Menu
                         join ml in db.MenuLanguage on m.Id equals ml.MenuId
                         join l in db.Language on ml.LanguageId equals l.Id
                         orderby m.Order ascending
                         where l.Code == language && m.Id != Id
                         select new MenuView
                         {
                             Id = m.Id,
                             Name = ml.Name,
                             isDropdown = m.isDropdown,
                             isActive = m.isActive,
                             Target = m.Target

                         }).Select(s => new SelectListItem
                         {
                             Text = s.Name,
                             Value = s.Id.ToString()
                         });
            ParentList.Add(new SelectListItem { Text = "isParent", Value = "0", Selected = true });
            ParentList.AddRange(items);
            return ParentList;
        }
    }
}
