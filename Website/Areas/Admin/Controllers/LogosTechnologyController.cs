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
using System.Net;
using System.Net.Mail;

namespace Website.Areas.Admin.Controllers
{
    public class LogosTechnologyController : BaseController
    {
        public LogosTechnologyService Service = new LogosTechnologyService();
        int Language { get; set; }
        // GET: Admin/LogosTechnology
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        
        public ActionResult GettAll()
        {
            List<LogosTechnologyVM> LogosTechnology = Service.GetAll();
            return Json(new { data = LogosTechnology }, JsonRequestBehavior.AllowGet);
        }
        
        // GET: Admin/LogosTechnology/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                LogosTechnologyVM LogosTechnology = Mapper.Map(Service.GetLogosTechnologyById(id),new LogosTechnologyVM());
                return PartialView("_Create", LogosTechnology);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/LogosTechnology/Create
        public ActionResult Create()
        {
            ViewBag.sort = GetLastSort();
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/LogosTechnology/Create
        [HttpPost]
        public ActionResult Create(LogosTechnology LogosTechnology)
        {
            try
            {
                bool result = SaveImages(LogosTechnology);
                if (LogosTechnology.Id == 0)
                {
                    // Save
                    Service.Post(LogosTechnology);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(LogosTechnology);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/LogosTechnology/Delete/5
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

        public bool SaveImages(LogosTechnology Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    LogosTechnology Item = new LogosTechnology();
                    if (file != null && file.ContentLength > 0)
                    {
                        string fname;
                        fname = file.FileName;
                        // Get the complete folder path and store the file inside it.    
                        string fileName = Path.GetFileName(fname);
                        string _name = FileHelper.GetFileNewNamewithoutfolder(fileName, Path.GetExtension(fileName));
                        Model.Image = _name;
                        string path = Path.Combine(Server.MapPath("~/"+ ConfigurationManager.AppSettings["LocalPath"] + "/"), _name);

                        file.SaveAs(path);
                        if (Model.Id != 0)
                        {
                            Item = Service.GetLogosTechnologyById(Model.Id);
                            List<string> images = new List<string>();
                            if (Item.Image != null)
                            {
                                images.AddRange(Item.Image.Split(',').ToList());
                                foreach (var item in images)
                                {
                                    string str = Path.Combine(Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                                    if (System.IO.File.Exists(str))
                                    {
                                        System.IO.File.Delete(str);
                                    }
                                }
                            }
                        }
                    }
                    else if (Model.Id != 0)
                    {
                        Item = Service.GetLogosTechnologyById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                LogosTechnology Item = Service.GetLogosTechnologyById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }

        //public ActionResult CheckIfsortExisting(int Sort)
        //{
        //    try
        //    {
        //        bool status = Service.CheckIfsortExisting(Sort);
        //        return Json(status, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult CheckIfsortExisting(int Id, string Sort)
        {
            try
            {
                bool status = Service.CheckIfsortExisting(Id, Sort);
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetLastSort()
        {
            int? sort = Service.GetLastSort();
            int newSort = sort == null ? 1 : sort.Value + 1;
            return newSort;
        }
    }
}
