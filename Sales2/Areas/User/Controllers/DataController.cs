using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Areas.User.Controllers
{
    public class DataController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User/Data
        public ActionResult Index()
        {
            return View();
        }

        public String GetUserName(string id)
        {
            var u = db.Users.Find(id);

            if(u != null)
            {
                return u.UserName;
            }
            else
            {
                return "";
            } 
        }
    }
}