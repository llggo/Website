using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Company.Areas.Manage.Controllers
{
    public class HomeController : Controller
    {
        Company.Models.ApplicationDbContext db = new Company.Models.ApplicationDbContext();
        
        // GET: Manage/Home
        [Authorize(Roles = "Admin, Support, Mod")]
        public async Task<ActionResult>  Index()
        {
            var customer = await db.Customer.ToListAsync();
            return View(customer);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Index");
        }
    }

}