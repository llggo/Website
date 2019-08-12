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
using PagedList;
using Microsoft.AspNet.Identity;

namespace Sales.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Product
        public async Task<ActionResult> Index(int? page)
        {
            return View(await db.Product.ToListAsync());
        }

        // GET: Manage/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Manage/Product/Create
        public ActionResult Create()
        {
            var model = new ProductCrud();
            model.Product.Visible = true;
            return View(model);
        }

        // POST: Manage/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Product,Images")] ProductCrud crud)
        {
            if (ModelState.IsValid)
            {
                crud.Product.Create = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };
                crud.Product.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Product.Add(crud.Product);

                db.SaveChanges();

                foreach (var i in crud.Images)
                {
                    var PImage = new ProductImage();
                    PImage.ProductId = crud.Product.Id;
                    PImage.Target = i;

                    db.ProductImage.Add(PImage);
                }

                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(crud);
        }

        // GET: Manage/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Manage/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CategoryId,Order,Visible,Price,Discount,Create")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Manage/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Product.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Manage/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Product.FindAsync(id);

            var images = (from i in db.ProductImage
                          where i.ProductId == id
                          select i);

            db.Product.Remove(product);
            db.ProductImage.RemoveRange(images);

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
