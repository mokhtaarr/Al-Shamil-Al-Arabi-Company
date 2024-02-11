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
    public class FeaturesController : BaseController
    {
        public FeaturesService Service = new FeaturesService();
        int Language { get; set; }
        // GET: Admin/Features
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            FeaturesVM GetFeatures = Service.GetFeatures();
            List<FeaturesVM> FeaturesVM = new List<FeaturesVM>();
            FeaturesVM.Add(GetFeatures);
            return Json(new { data = FeaturesVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Features/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                Features Features = Service.GetFeaturesById(id);
                return PartialView("_Create", Features);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Features/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/Features/Create
        [HttpPost]
        public ActionResult Create(Features Features)
        {
            try
            {
                if (Features.Id == 0)
                {
                    // Save
                    Service.Post(Features);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(Features);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Features/Delete/5
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
