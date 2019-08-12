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
    public class OptionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Option/Edit/5
        public async Task<ActionResult> Edit()
        {
            OptionModel optionModel = await db.Option.FirstOrDefaultAsync();
            if (optionModel == null)
            {
                return HttpNotFound();
            }
            return View(optionModel);
        }

        // POST: Manage/Option/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Logo,Address,Phone,Fax,Email,Infomation,Facbook,Youtube,Twitter,Google,Edit,Create")] OptionModel optionModel)
        {
            if (ModelState.IsValid)
            {
                optionModel.Edit = new Modified { Time = DateTime.Now, User = User.Identity.GetUserId() };

                db.Entry(optionModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Edit");
            }
            return View(optionModel);
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
