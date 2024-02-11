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
    public class AboutUsController : BaseController
    {
        public AboutUsService Service = new AboutUsService();
        int Language { get; set; }
        // GET: Admin/AboutUs
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            var GetAboutUs = Service.GetAboutUs();
            List<AboutUsVM> AboutUsVM = new List<AboutUsVM>();
            AboutUsVM.Add(GetAboutUs);
            return Json(new { data = AboutUsVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/AboutUs/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                AboutUs AboutUs = Service.GetAboutUsById(id);
                return PartialView("_Create", AboutUs);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/AboutUs/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/AboutUs/Create
        [HttpPost]
        public ActionResult Create(AboutUs AboutUs)
        {
            try
            {
                bool result = SaveImages(AboutUs);
                if (AboutUs.Id == 0)
                {
                    // Save
                    Service.Post(AboutUs);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(AboutUs);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/AboutUs/Delete/5
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

        public bool SaveImages(AboutUs Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    AboutUs Item = new AboutUs();
                    if (file != null && file.ContentLength > 0)
                    {
                        string fname;
                        fname = file.FileName;
                        // Get the complete folder path and store the file inside it.    
                        string fileName = Path.GetFileName(fname);
                        string _name = FileHelper.GetFileNewNamewithoutfolder(fileName, Path.GetExtension(fileName));
                        Model.Image += _name + ",";
                        string path = Path.Combine(Server.MapPath("~/"+ ConfigurationManager.AppSettings["LocalPath"] + "/"), _name);

                        file.SaveAs(path);
                        if (Model.Id != 0)
                        {
                            Item = Service.GetAboutUsById(Model.Id);
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
                        Item = Service.GetAboutUsById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                var Item = Service.GetAboutUsById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }
    }
}
