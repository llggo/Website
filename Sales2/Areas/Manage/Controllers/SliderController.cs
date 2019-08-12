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
    public class SliderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Slider
        public async Task<ActionResult> Index()
        {
            return View(await db.Slider.ToListAsync());
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
            var model = new Slider();
            model.Visible = true;
            model.Image = "https://dummyimage.com/1920x720/000000/15ff00&text=thanghoafoods";

            return View(model);
        }

        // POST: Manage/Slider/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Image,Order,Visible,Content,Target")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Create = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };
                slider.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Slider.Add(slider);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Manage/Slider/Edit/5
        public async Task<ActionResult> Edit(int? id)
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

        // POST: Manage/Slider/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Image,Order,Visible,Content,Target,Create")] Slider slider)
        {
            if (ModelState.IsValid)
            {
                slider.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Entry(slider).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(slider);
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
