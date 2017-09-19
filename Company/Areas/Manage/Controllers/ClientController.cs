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
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Manage/Client
        public  ActionResult Index()
        {
            return RedirectToAction("Edit", "Option", new { area = "Manage" });
        }

        // GET: Manage/Client/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Client.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Manage/Client/Create
        public ActionResult Create()
        {
            var model = new CrudClient();
            return View(model);
        }

        // POST: Manage/Client/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Client,ClientLanguage,Language")] CrudClient cc)
        {
            if (ModelState.IsValid)
            {
                cc.Client.CreateTime = cc.Client.LastEditTime = DateTime.Now;
                cc.Client.CreateUser = cc.Client.LastEditUser = User.Identity.GetUserId();
                db.Client.Add(cc.Client);
                await db.SaveChangesAsync();

                foreach (var cl in cc.ClientLanguage)
                {
                    cl.Value.ClientId = cc.Client.Id;
                    db.ClientLanguage.Add(cl.Value);
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit","Option", new { area = "Manage"});
            }

            return View(cc);
        }

        // GET: Manage/Client/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var client = await db.Client.FindAsync(id);

            if (client == null)
            {
                return HttpNotFound();
            }


            var clientlanguage = await (from cl in db.ClientLanguage
                                         where cl.ClientId == client.Id
                                         select cl).ToListAsync();

            var cld = new Dictionary<string, ClientLanguage>() { };

            foreach (var cl in clientlanguage)
            {
                var langcode = await (from lc in db.Language
                                      where lc.Id == cl.LanguageId
                                      select lc).FirstOrDefaultAsync();

                cld.Add(langcode.Code, cl);
            }

            var cc = new CrudClient()
            {
                Client = client,
                ClientLanguage = cld
            };

            return View(cc);
        }

        // POST: Manage/Client/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Client,ClientLanguage,Language")] CrudClient cc)
        {
            if (ModelState.IsValid)
            {
                cc.Client.LastEditTime = DateTime.Now;
                cc.Client.LastEditUser = User.Identity.GetUserId();
                db.Entry(cc.Client).State = EntityState.Modified;
                await db.SaveChangesAsync();

                foreach (var cl in cc.ClientLanguage)
                {
                    db.Entry(cl.Value).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();

                return RedirectToAction("Edit", "Option", new { area = "Manage" });
            }
            return View(cc);
        }

        // GET: Manage/Client/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Client.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Manage/Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Client.FindAsync(id);
            db.Client.Remove(client);

            var clientlanguage = await (from cl in db.ClientLanguage
                                          where cl.ClientId == client.Id
                                          select cl).ToListAsync();

            foreach (var cl in clientlanguage)
            {
                db.ClientLanguage.Remove(cl);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Edit", "Option", new { area = "Manage" });
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
