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
using Website.Areas.Admin.Data;

namespace Website.Areas.Admin.Controllers
{
    [CustomAuthenticationFilter]
    public class CategoiesController : BaseController
    {
        public CategoriesService categoriesApi = new CategoriesService();
        int Language { get; set; }
        // GET: Admin/Categoies
        public ActionResult Index()
        {
            Language = GetLanguage();
            return View();
        }

        public ActionResult GettAll()
        {
            return Json(new { data = categoriesApi.GetCategories(Language)}, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Categoies/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Language = GetLanguage();
                Category category = categoriesApi.GetCategory(id);
                CategoryVM categoryVM =  Mapper.Map(category, new CategoryVM());
                ViewBag.Categories = categoriesApi.GetCategories(Language).Where(x => x.Id != id).Select(x => new CategoryVM
                {
                    Id = x.Id,
                    Name = Language == (int)LanuageType.AR ? x.ArName : x.EnName
                });

                ViewBag.CategoryCnotent = categoriesApi.GetCategoryCnotent(Language, id);
                return PartialView("Create", categoryVM);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Admin/Categoies/Create
        public ActionResult Create()
        {
            Language = GetLanguage();
            ViewBag.sort = GetLastSort();
            ViewBag.Categories = categoriesApi.GetCategories(Language).Select(x => new CategoryVM
            {
                Id = x.Id,
                Name = Language == (int)LanuageType.AR ? x.ArName : x.EnName
            });
            return PartialView("Create");
        }

        // POST: Admin/Categoies/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                if (category.Id == 0)
                {
                    // Save
                    categoriesApi.Post(category);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Edit
                    categoriesApi.Put(category);
                    return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Admin/Categoies/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                categoriesApi.Delete(id);
                return Json(new { Status = true }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult IsSortExist(int Id, string Sort)
        {
            bool res = categoriesApi.IsSortExist(Id, Sort);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public int GetLastSort()
        {
            int? sort = categoriesApi.GetLastSort();
            int newSort = sort == null ? 1 : sort.Value + 1;
            return newSort;
        }
    }
}
