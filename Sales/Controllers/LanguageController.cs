using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sales.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Change(string culture, string returnUrl)
        {
           
            var httpCookie = Request.Cookies["language"];
            if (httpCookie != null)
            {
                var cookie = Response.Cookies["language"];
                if (cookie != null) cookie.Value = culture;
            }

            

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", routeValues: new { area = "" });
        }
    }
}