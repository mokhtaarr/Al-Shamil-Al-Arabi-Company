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
    public class SubFeaturesController : BaseController
    {
        public SubFeaturesService Service = new SubFeaturesService();
        int Language { get; set; }
        // GET: Admin/SubFeatures
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            List<SubFeaturesVM> GetSubFeatures = Service.GetSubFeatures();
            return Json(new { data = GetSubFeatures }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/SubFeatures/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                SubFeatures SubFeatures = Service.GetSubFeaturesById(id);
                ViewBag.Features = Service.GetAllFeatures().Where(x => x.Id != id).Select(x => new _SelectList
                {
                    Id = x.Id,
                    Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
                });
                return PartialView("_Create", SubFeatures);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/SubFeatures/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            ViewBag.Features = Service.GetAllFeatures().ToList().Select(x => new _SelectList
            {
                Id = x.Id,
                Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
            });
            return PartialView("_Create");
        }

        // POST: Admin/SubFeatures/Create
        [HttpPost]
        public ActionResult Create(SubFeatures SubFeatures)
        {
            try
            {
                bool result = SaveImages(SubFeatures);
                if (SubFeatures.Id == 0)
                {
                    // Save
                    Service.Post(SubFeatures);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(SubFeatures);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/SubFeatures/Delete/5
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

        public bool SaveImages(SubFeatures Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    SubFeatures Item = new SubFeatures();
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
                            Item = Service.GetSubFeaturesById(Model.Id);
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
                        Item = Service.GetSubFeaturesById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                var Item = Service.GetSubFeaturesById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }
    }
}
