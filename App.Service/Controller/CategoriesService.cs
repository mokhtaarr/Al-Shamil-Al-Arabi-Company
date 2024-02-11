using AutoMapper;
using App.Service.Repository;
using App.Service.UnitOfWork;
using App.VM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using App.Dal.Context;
using App.Dal.Model;
using App.Static.Enums;

namespace App.Service.Controllers
{
    public class CategoriesService
    {
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Category> repository;
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        public CategoriesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Category>(unitOfWork);
        }

        // GET: api/Categories
        public List<CategoryVM> GetCategories(int Language)
        {
            try
            {
                List<CategoryVM> Categories = repository.GetAll().Select(x=> new CategoryVM {
                    CategoryID = x.CategoryID,
                    Id = x.Id,
                    ArName = x.ArName,
                    EnName = x.EnName,
                    //CategoryCnotent =x.CategoryCnotent 
                }).ToList();
                if (Categories == null || !Categories.Any())
                {
                    return null;
                }
                //List<CategoryVM> categoryVM = Mapper.Map(Categories, new List<CategoryVM>());
                return Categories;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Categories
        public List<CategoryCnotent> GetCategoryCnotent(int Language, int Id)
        {
            try
            {
                List<CategoryCnotent> CategoryCnotent = db.CategoryCnotents.Where(x=>x.CategoryID == Id).ToList();
                return CategoryCnotent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Categories/5
        public Category GetCategory(int id)
        {
            Category category = repository.Get(id);
            if (category == null)
            {
                return null;
            }
            return category;
        }

        // POST: api/Categories
        public void Post(Category category)
        {
            try
            {
                repository.Add(category);
                unitOfWork.Save();
                foreach (CategoryCnotent item in category.CategoryCnotent) {item.CategoryID = category.Id; }
                repository.AddCategoryCnotent(category.CategoryCnotent);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/Categories/5
        public void Put(Category model)
        {
            try
            {
                Category category = repository.Get(model.Id);
                repository.Update(model, category);
                unitOfWork.Save();
                List<CategoryCnotent> categoryCnotents = model.CategoryCnotent.ToList();
                UpdateCategoryCnotent(categoryCnotents, model.Id);
            }
            catch(Exception dd) 
            {
                unitOfWork.Rollback();
            }
        }

        public void UpdateCategoryCnotent(List<CategoryCnotent> models,int Id)
        {
            try
            {
                var insert = models.Where(x => x.Id == 0).ToList();
                var update = models.Where(x => x.Id != 0).ToList();

                foreach (CategoryCnotent item in models) { item.CategoryID = Id; }
                db.CategoryCnotents.AddRange(insert);
                foreach (var item in update)
                {
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (Exception dd)
            {
                unitOfWork.Rollback();
            }
        }

        // DELETE: api/Categories/5
        public void Delete(int id)
        {
            try
            {
                Category category = repository.Get(id);
                repository.Remove(category);
                unitOfWork.Save();

                List<CategoryCnotent> categoryCnotents = db.CategoryCnotents.Where(x => x.CategoryID == id).ToList();
                db.CategoryCnotents.RemoveRange(categoryCnotents);
                db.SaveChanges();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }

        public List<CategoryVM> GetCategoriesForMinus(int Language)
        {
            List<CategoryVM> Categories = repository.Find(x => x.CategoryID == null).Select(x => new CategoryVM
            {
                CategoryID = x.CategoryID,
                Id = x.Id,
                Sort = x.Sort,
                AllCategories = x.AllCategories,
                Name = Language == (int)LanuageType.AR ? x.ArName : x.EnName,
            }).OrderBy(x => x.Sort).ToList();
            return Categories;
        }

        public bool IsSortExist(int Id, string Sort)
        {
            int _Sort = Sort == "" ? 0 : int.Parse(Sort);
            var IsExistSort = repository.Find(u => u.Sort == _Sort && u.Id != Id).FirstOrDefault();
            return IsExistSort == null ? true : false;
        }

        public int? GetLastSort()
        {
            int? sort =  repository.GetAll().Select(x=>x.Sort).OrderByDescending(x => x)?.FirstOrDefault();
            return sort;
        }
    }
}
