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

    public class CompanyWordController : BaseController
    {
        public CompanyWordService Service = new CompanyWordService();
        int Language { get; set; }
        // GET: Admin/CompanyWord
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            var GetCompanyWord = Service.GetCompanyWord();
            List<CompanyWordVM> CompanyWordVM = new List<CompanyWordVM>();
            CompanyWordVM.Add(GetCompanyWord);
            return Json(new { data = CompanyWordVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/CompanyWord/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                CompanyWord companyWord = Service.GetCompanyWordById(id);
                return PartialView("_Create", companyWord);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/CompanyWord/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/CompanyWord/Create
        [HttpPost]
        public ActionResult Create(CompanyWord companyWord)
        {
            try
            {
                bool result = SaveImages(companyWord);
                if (companyWord.Id == 0)
                {
                    // Save
                    Service.Post(companyWord);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(companyWord);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/CompanyWord/Delete/5
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

        public bool SaveImages(CompanyWord Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    CompanyWord Item = new CompanyWord();
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
                            Item = Service.GetCompanyWordById(Model.Id);
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
                        Item = Service.GetCompanyWordById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                CompanyWord Item = Service.GetCompanyWordById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }
    }
}
