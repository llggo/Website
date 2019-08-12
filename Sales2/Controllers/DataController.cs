using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Controllers
{
    public class DataController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {

            var menu = from m in db.Menu
                       orderby m.Order ascending
                       where (m.Visible == true & m.IsRoot)
                       select m;

            return PartialView(menu.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult ChildrenMenu(int ParentId)
        {
            var menu = from m in db.Menu
                       orderby m.Order ascending
                       where (m.Visible == true & m.ParentId == ParentId)
                       select m;

            return PartialView(menu.ToList());
        }
    }
}