using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Service.Controllers;
using Website.Controllers;
using App.Dal.Model;
using AutoMapper;
using App.VM;
using App.Static.Enums;
using App.Static.Files;
using System.IO;
using System.Configuration;
using Website.Areas.Admin.Data;

namespace Website.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class PortfolioController : BaseController
    {
        public PortfolioService Service = new PortfolioService();
        int Language { get; set; }
        // GET: Admin/Portfolio
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            PortfolioVM GetPortfolio = Service.GetPortfolio();
            List<PortfolioVM> PortfolioVM = new List<PortfolioVM>();
            PortfolioVM.Add(GetPortfolio);
            return Json(new { data = PortfolioVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Portfolio/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                Portfolio Portfolio = Service.GetPortfolioById(id);
                return PartialView("_Create", Portfolio);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Portfolio/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/Portfolio/Create
        [HttpPost]
        public ActionResult Create(Portfolio Portfolio)
        {
            try
            {
                if (Portfolio.Id == 0)
                {
                    // Save
                    Service.Post(Portfolio);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(Portfolio);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Portfolio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Service.Delete(id);
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
