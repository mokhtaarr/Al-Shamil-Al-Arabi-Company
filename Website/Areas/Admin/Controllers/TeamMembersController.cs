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
using App.Static.Files;
using System.IO;
using System.Configuration;
using Website.Areas.Admin.Data;
using App.Static.Enums;

namespace Website.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class TeamMembersController : BaseController
    {
        public TeamMembersService Service = new TeamMembersService();
        int Language { get; set; }
        // GET: Admin/TeamMembers
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            List<TeamMembersVM> GetTeamMembers = Service.GetTeamMembers();
            return Json(new { data = GetTeamMembers }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/TeamMembers/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                TeamMembers TeamMembers = Service.GetTeamMembersById(id);
                ViewBag.Team = Service.GetAllTeam().Where(x => x.Id != id).Select(x => new _SelectList
                {
                    Id = x.Id,
                    Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
                });
                return PartialView("_Create", TeamMembers);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/TeamMembers/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            ViewBag.Team = Service.GetAllTeam().ToList().Select(x => new _SelectList
            {
                Id = x.Id,
                Text = Language == (int)LanuageType.AR ? x.ArDescription : x.EnDescription
            });
            return PartialView("_Create");
        }

        // POST: Admin/TeamMembers/Create
        [HttpPost]
        public ActionResult Create(TeamMembers TeamMembers)
        {
            try
            {
                bool result = SaveImages(TeamMembers);
                if (TeamMembers.Id == 0)
                {
                    // Save
                    Service.Post(TeamMembers);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(TeamMembers);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/TeamMembers/Delete/5
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

        public bool SaveImages(TeamMembers Model)
        {
            Model.Image = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    TeamMembers Item = new TeamMembers();
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
                            Item = Service.GetTeamMembersById(Model.Id);
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
                        Item = Service.GetTeamMembersById(Model.Id);
                        Model.Image = Item.Image;
                    }
                }
            }
            else if (Model.Id != 0 && (Model.Image == "" || Model.Image == null))
            {
                var Item = Service.GetTeamMembersById(Model.Id);
                Model.Image = Item.Image;
            }
            return true;
        }
    }
}
