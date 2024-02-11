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
    public class TeamController : BaseController
    {
        public TeamService Service = new TeamService();
        int Language { get; set; }
        // GET: Admin/Team
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }
        public ActionResult GettAll()
        {
            TeamVM GetTeam = Service.GetTeam();
            List<TeamVM> TeamVM = new List<TeamVM>();
            TeamVM.Add(GetTeam);
            return Json(new { data = TeamVM }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Team/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                Team Team = Service.GetTeamById(id);
                return PartialView("_Create", Team);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Team/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            return PartialView("_Create");
        }

        // POST: Admin/Team/Create
        [HttpPost]
        public ActionResult Create(Team Team)
        {
            try
            {
                if (Team.Id == 0)
                {
                    // Save
                    Service.Post(Team);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    Service.Put(Team);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Team/Delete/5
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
