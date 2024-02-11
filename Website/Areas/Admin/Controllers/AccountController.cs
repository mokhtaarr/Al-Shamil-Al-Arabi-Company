using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Dal.Model;
using App.Service.Controllers;
using App.VM;

namespace Website.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public UsersService Service = new UsersService();
        readonly ContactUsService contact = new ContactUsService();
        // GET: /Account/Login
        public ActionResult Login()
        {
            if (Request.Cookies["Logo"] == null)
                GetLogo();

            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(Users users)
        {
            if (ModelState.IsValid)
            {
                Users user = Service.LogIn(users.UserName, users.Password);
                if(user != null)
                    CreateCookie(user.Id.ToString(), user.UserName);
                bool res = user == null ? false : true;
                return Json(res, JsonRequestBehavior.AllowGet);
            }else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult changePassword(string returnUrl)
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult changePassword(Users users)
        {
            HttpCookie userId = Request.Cookies["userId"];
            int id = userId != null ? int.Parse(userId.Value) : 0;
            users.Id = id;
            if (id == 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
            {
                HttpCookie userName = Request.Cookies["userName"];
                users.UserName = userName.Value.ToString();
                bool res = Service.ChangeUserInfo(users);
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            try
            {
                RemoveCookie();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        private void CreateCookie(string userId, string userName)
        {
            HttpCookie id = new HttpCookie("userId", userId);
            HttpCookie name = new HttpCookie("userName", userName);
            id.Expires = DateTime.Now.AddHours(1);
            name.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(id);
            Response.Cookies.Add(name);
        }

        private void RemoveCookie()
        {
            if (Request.Cookies["userId"] != null)
            {
                HttpCookie id = new HttpCookie("userId");
                id.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(id);
            }
            if (Request.Cookies["userName"] != null)
            {
                HttpCookie userName = new HttpCookie("userName");
                userName.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(userName);
            }
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