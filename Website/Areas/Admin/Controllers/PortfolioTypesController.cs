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
    public class PortfolioTypesController : BaseController
    {
        public PortfolioTypesService Service = new PortfolioTypesService();
        int Language { get; set; }
        // GET: Admin/PortfolioTypes
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            List<PortfolioTypesVM> GetPortfolioTypes = Service.GetPortfolioTypes();
            return Json(new { data = GetPortfolioTypes }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/PortfolioTypes/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                PortfolioType PortfolioTypes = Service.GetPortfolioTypesById(id);
                return PartialView("_Create", PortfolioTypes);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/PortfolioTypes/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/PortfolioTypes/Create
        [HttpPost]
        public ActionResult Create(PortfolioType PortfolioTypes)
        {
            try
            {
                if (PortfolioTypes.Id == 0)
                {
                    // Save
                    Service.Post(PortfolioTypes);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(PortfolioTypes);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/PortfolioTypes/Delete/5
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
