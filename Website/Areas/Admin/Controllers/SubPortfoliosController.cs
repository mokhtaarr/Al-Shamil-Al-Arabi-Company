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
    public class SubPortfoliosController : BaseController
    {
        public SubPortfoliosService Service = new SubPortfoliosService();
        int Language { get; set; }
        // GET: Admin/SubPortfolio
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            List<SubPortfolioVM> GetSubPortfolio = Service.GetSubPortfolios();
            return Json(new { data = GetSubPortfolio }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/SubPortfolio/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                SubPortfolios SubPortfolio = Service.GetSubPortfoliosById(id);
                ViewBag.Portfolio = Service.GetAllPortfolios().Select(x => new _SelectList
                {
                    Id = x.Id,
                    Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
                });
                ViewBag.PortfolioType = Service.GetAllPortfolioTypes().ToList().Select(x => new _SelectList
                {
                    Id = x.Id,
                    Text = Language == (int)LanuageType.AR ? x.ArName : x.EnName
                });
                return PartialView("_Create", SubPortfolio);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/SubPortfolio/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            ViewBag.Portfolio = Service.GetAllPortfolios().ToList().Select(x => new _SelectList
            {
                Id = x.Id,
                Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
            });
            ViewBag.PortfolioType = Service.GetAllPortfolioTypes().ToList().Select(x => new _SelectList
            {
                Id = x.Id,
                Text = Language == (int)LanuageType.AR ? x.ArName : x.EnName
            });
            return PartialView("_Create");
        }

        // POST: Admin/SubPortfolio/Create
        [HttpPost]
        public ActionResult Create(SubPortfolios SubPortfolio)
        {
            try
            {
                bool result = SaveImages(SubPortfolio);
                if (SubPortfolio.Id == 0)
                {
                    // Save
                    Service.Post(SubPortfolio);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(SubPortfolio);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/SubPortfolio/Delete/5
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

        public bool SaveImages(SubPortfolios Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    SubPortfolios Item = new SubPortfolios();
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
                            Item = Service.GetSubPortfoliosById(Model.Id);
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
                        Item = Service.GetSubPortfoliosById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                var Item = Service.GetSubPortfoliosById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }
    }
}
