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
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/ProductImage
        public async Task<ActionResult> Index()
        {
            return View(await db.ProductImage.ToListAsync());
        }

        // GET: Manage/ProductImage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: Manage/ProductImage/Create
        public ActionResult Create()
        {
            ViewBag.ProductList = ProductList();
            return View();
        }

        // POST: Manage/ProductImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductId,Name,Path")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.ProductImage.Add(productImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productImage);
        }

        // GET: Manage/ProductImage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: Manage/ProductImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductId,Name,Path")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productImage);
        }

        // GET: Manage/ProductImage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: Manage/ProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductImage productImage = await db.ProductImage.FindAsync(id);
            db.ProductImage.Remove(productImage);
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

        public List<SelectListItem> ProductList()
        {
            var category = from c in db.Product
                           join cl in db.ProductLanguage on c.Id equals cl.ProductId
                           join l in db.Language on cl.LanguageId equals l.Id
                           where l.Code == language
                           select new SelectListItem
                           {
                               Text = cl.Name,
                               Value = c.Id.ToString()
                           };
            List<SelectListItem> item = new List<SelectListItem>();
            item.AddRange(category);
            return item;
        }
    }
}
