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
    public class ServicesController : BaseController
    {
        public ServicesService Service = new ServicesService();
        int Language { get; set; }
        // GET: Admin/Services
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            ServicesVM GetServices = Service.GetServices();
            List<ServicesVM> ServicesVM = new List<ServicesVM>();
            ServicesVM.Add(GetServices);
            return Json(new { data = ServicesVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Services/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                Services Services = Service.GetServicesById(id);
                return PartialView("_Create", Services);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Services/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/Services/Create
        [HttpPost]
        public ActionResult Create(Services Services)
        {
            try
            {
                if (Services.Id == 0)
                {
                    // Save
                    Service.Post(Services);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(Services);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Services/Delete/5
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
