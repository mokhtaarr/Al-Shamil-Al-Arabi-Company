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
using System.Web;

namespace App.Service.Controllers
{
    public class SubFeaturesService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<SubFeatures> repository;

        public SubFeaturesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<SubFeatures>(unitOfWork);
        }

       
        // GET: api/SubFeatures
        public List<SubFeaturesVM> GetSubFeatures()
        {
            try
            {
                List<SubFeatures> SubFeatures = repository.GetAll().ToList();
                if (SubFeatures == null)
                {
                    return null;
                }
                return Mapper.Map(SubFeatures, new List<SubFeaturesVM>());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Features> GetAllFeatures()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<Features> Features = db.Features.ToList();
                    if (Features == null)
                    {
                        return null;
                    }
                    return Features;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/SubFeatures/5
        public SubFeatures GetSubFeaturesById(int id)
        {
            SubFeatures SubFeatures = repository.Get(id);
            if (SubFeatures == null)
            {
                return null;
            }
            return SubFeatures;
        }

        // POST: api/SubFeatures
        public void Post(SubFeatures SubFeatures)
        {
            try
            {
                repository.Add(SubFeatures);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/SubFeatures/5
        public void Put(SubFeatures model)
        {
            try
            {
                SubFeatures SubFeatures = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (SubFeatures != null)
                    {
                        model.Image = model.Image == null ? SubFeatures.Image : model.Image;
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception dd)
            {
                unitOfWork.Rollback();
            }
        }

        // DELETE: api/SubFeatures/5
        public void Delete(int id)
        {
            try
            {
                SubFeatures SubFeatures = repository.Get(id);
                List<string> images = new List<string>();
                if (SubFeatures.Image != null)
                {
                    images.AddRange(SubFeatures.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(SubFeatures);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
