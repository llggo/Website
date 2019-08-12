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
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        

        // GET: Manage/Menu
        public async Task<ActionResult> Index()
        {
            return View(await db.Menu.ToListAsync());
        }

        // GET: Manage/Menu/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuModel menuModel = await db.Menu.FindAsync(id);
            if (menuModel == null)
            {
                return HttpNotFound();
            }
            return View(menuModel);
        }

        // GET: Manage/Menu/Create
        public ActionResult Create()
        {
            var model = new MenuModel();
            model.Visible = true;

            return View(model);
        }

        // POST: Manage/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,IsRoot,IsParent,IsDropdown,ParentId,Order,Target,Visible")] MenuModel menuModel)
        {
            if (ModelState.IsValid)
            {
                menuModel.Create = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };
                menuModel.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Menu.Add(menuModel);



                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(menuModel);
        }

        // GET: Manage/Menu/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuModel menuModel = await db.Menu.FindAsync(id);
            if (menuModel == null)
            {
                return HttpNotFound();
            }
            return View(menuModel);
        }

        // POST: Manage/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,IsRoot,IsParent,IsDropdown,ParentId,Order,Target,Visible,,Create")] MenuModel menuModel)
        {
            
            if (ModelState.IsValid)
            {
                menuModel.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Entry(menuModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuModel);
        }

        // GET: Manage/Menu/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MenuModel menuModel = await db.Menu.FindAsync(id);
            if (menuModel == null)
            {
                return HttpNotFound();
            }
            return View(menuModel);
        }

        // POST: Manage/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MenuModel menuModel = await db.Menu.FindAsync(id);

            db.Menu.Remove(menuModel);


            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        

        public String GetMenuName(int? id)
        {
            if (id == null) return "Is Parent";

            var menu = db.Menu.Find(id);

            if (menu != null)
                return menu.Name;
            else
            {
                return "Null";
            }
            
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
