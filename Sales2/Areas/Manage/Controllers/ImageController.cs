using Sales.Areas.Manage.Models;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Areas.Manage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Manage/Image
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Image/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: Manage/Image/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: Manage/Image/Create
        [HttpPost]
        public bool Create(int productId, string target)
        {
            var pi = new ProductImage();
            pi.ProductId = productId;
            pi.Target = target;

            db.ProductImage.Add(pi);

            db.SaveChanges();

            return true;
        }

        // GET: Manage/Image/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Image/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // post: Manage/Image/Delete/5

        [HttpPost]
        public bool Delete(int id)
        {
            var i = db.ProductImage.Find(id);
            db.ProductImage.Remove(i);
            db.SaveChanges();

            return true;
        }

        // POST: Manage/Image/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
