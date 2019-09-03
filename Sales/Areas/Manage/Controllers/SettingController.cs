using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Areas.Manage.Controllers
{
    [Authorize]
    public class SettingController : Controller
    {
        // GET: Manage/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}