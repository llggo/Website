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
    public class SpecificProductsController2 : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Manage/Option/Edit/5
        public async Task<ActionResult> Edit()
        {
            var sproduct = await db.SProduct.FirstOrDefaultAsync();

            if (sproduct == null)
            {
                return HttpNotFound();
            }
            var sproductlanguage = await (from ol in db.SProductLanguage
                                      where ol.SProductId == sproduct.Id
                                      select ol).ToListAsync();
            var old = new Dictionary<string, SProductLanguage>() { };
            foreach (var m in sproductlanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == m.LanguageId
                                      select lc).FirstOrDefaultAsync();
                old.Add(langcode.Code, m);
            }


            var em = new CrudSProduct()
            {
                SProduct = sproduct,
                SProductLanguage = old
            };


            return View(em);
        }

        // POST: Manage/Option/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SProduct,Language,SProductLanguage")] CrudSProduct CrudSProduct)
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
