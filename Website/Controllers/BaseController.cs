using App.Static.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public int GetLanguage()
        {
            HttpCookie cookie = Request.Cookies["Language"];
            if (cookie == null)
            {
                cookie = new HttpCookie("Language");
                cookie.Value = LanuageType.AR.GetDisplayName();
                Response.Cookies.Add(cookie);
                return (int)LanuageType.AR;
            }
            else
            {
                if (LanuageType.AR.ToString() == cookie.Value.ToString())
                {
                    return (int)LanuageType.AR;
                }
                else if (LanuageType.EN.ToString() == cookie.Value.ToString())
                {
                    return (int)LanuageType.EN;
                }else
                    return (int)LanuageType.AR;
            }
        }

        public bool IsAuthorized()
        {
            HttpCookie userId = Request.Cookies["userId"];
            if (userId != null)
                return true;
            else
                return false;
        }
    }
}