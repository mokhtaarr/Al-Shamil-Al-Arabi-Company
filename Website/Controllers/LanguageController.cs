using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class LanguageController : BaseController
    {
        // GET: Language
        public ActionResult ChangeLanguage(string SelectedLanguage, string returnURL)
        {
            if (SelectedLanguage != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(SelectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(SelectedLanguage);
                var cookie = new HttpCookie("Language");
                cookie.Value = SelectedLanguage;
                Response.Cookies.Remove("Language");
                Response.Cookies.Add(cookie);

                int lang = GetLanguage();
            }
            return Json("");
        }
    }
}