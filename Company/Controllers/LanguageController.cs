using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Change(string culture, string returnUrl)
        {
            var cookie_request = Request.Cookies["language"];
            if (cookie_request != null)
            {
                var cookie_response = Response.Cookies["language"];
                if (cookie_response != null) cookie_response.Value = culture;
            }
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home", routeValues: new { area = "" });
        }
    }
}