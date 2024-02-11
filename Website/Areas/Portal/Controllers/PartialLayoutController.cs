using App.Service.Controllers;
using App.Dal.Model;
using App.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Controllers;
using App.Static.Enums;

namespace Website.Areas.Portal.Controllers
{
    public class PartialLayoutController : BaseController
    {
        int Language = 0;
        public CategoriesService categoriesApi = new CategoriesService();
        public ContactUsService contact  = new ContactUsService();
        // GET: Portal/PartialLayout
        public PartialViewResult Header()
        {
            Language = GetLanguage();
            if (Request.Cookies["Logo"] == null)
                GetLogo();

            ViewBag.lan = Language;
            List<CategoryVM> categories = categoriesApi.GetCategoriesForMinus(Language).ToList();
            return PartialView("header", categories);
        }
        public PartialViewResult Footer()
        {
            Language = GetLanguage();
            ContactUsVM contacts = contact.GetContactUsForHome(Language);
            return PartialView("_Footer", contacts);
        }

        public void GetLogo()
        {
            ContactUsVM contactUs = contact.GetContactUs();
            if (contactUs != null)
            {
                CreateLogoCookie(contactUs.Image);
            }
        }
        private void CreateLogoCookie(string logo)
        {
            HttpCookie Logo = new HttpCookie("Logo", logo);
            Logo.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(Logo);
        }
    }
}