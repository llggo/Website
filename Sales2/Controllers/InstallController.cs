using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Controllers
{
    public class InstallController : Controller
    {
        // GET: Install
        public ActionResult Index()
        {
            
                var userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roleManager = System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();

                string[] admin = { "admin@example.com" };

                const string password = "Admin@123456";

                string[] roleName = { "Admin" };

                foreach (var r in roleName)
                {
                    //Create Role Admin if it does not exist
                    var role = roleManager.FindByName(r);
                    if (role == null)
                    {
                        role = new IdentityRole(r);
                        var roleresult = roleManager.Create(role);
                    }

                }

                foreach (var a in admin)
                {
                    var user = userManager.FindByName(a);
                    if (user == null)
                    {
                        user = new ApplicationUser { UserName = a, Email = a };
                        var result = userManager.Create(user, password);
                        result = userManager.SetLockoutEnabled(user.Id, false);
                    }

                    var rolesForUser = userManager.GetRoles(user.Id);
                    if (!rolesForUser.Contains(roleName[0]))
                    {
                        var result = userManager.AddToRole(user.Id, roleName[0]);
                    }
                }

                return null;
            
        }
    }
}