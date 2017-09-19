using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Company.Models;
using System.Threading;

namespace Company.Controllers
{
    public class DataController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        string language = Thread.CurrentThread.CurrentCulture.IetfLanguageTag;

        // GET: Data
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Menu()
        {

            var menu = from m in db.Menu
                       join ml in db.MenuLanguage on m.Id equals ml.MenuId
                       join l in db.Language on ml.LanguageId equals l.Id
                       orderby m.Order ascending
                       where (l.Code == language && m.Visible == true & (m.ParentId == 0 || m.ParentId == null))
                       select new MenuView
                       {
                           Id = m.Id,
                           Name = ml.Name,
                           isDropdown = m.isDropdown,
                           isActive = m.isActive,
                           Target = m.Target

                       };
            return PartialView(menu.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult ChildrenMenu(int ParentId)
        {
            var menu = from m in db.Menu
                       join ml in db.MenuLanguage on m.Id equals ml.MenuId
                       join l in db.Language on ml.LanguageId equals l.Id
                       orderby m.Order ascending
                       where (l.Code == language && m.Visible == true & m.ParentId == ParentId)
                       select new MenuView
                       {
                           Id = m.Id,
                           Name = ml.Name,
                           isDropdown = m.isDropdown,
                           isActive = m.isActive,
                           Target = m.Target

                       };
            return PartialView(menu.ToList());
        }
    }
}