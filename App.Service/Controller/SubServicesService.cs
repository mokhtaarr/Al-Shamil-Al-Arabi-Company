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
    public class SubServicesService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<SubServices> repository;

        public SubServicesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<SubServices>(unitOfWork);
        }

        
        // GET: api/SubServices
        public List<SubServicesVM> GetSubServices()
        {
            try
            {
                List<SubServices> SubServices = repository.GetAll().ToList();
                if (SubServices == null)
                {
                    return null;
                }
                return Mapper.Map(SubServices, new List<SubServicesVM>());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Services> GetAllServices()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<Services> Services = db.Services.ToList();
                    if (Services == null)
                    {
                        return null;
                    }
                    return Services;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/SubServices/5
        public SubServices GetSubServicesById(int id)
        {
            SubServices SubServices = repository.Get(id);
            if (SubServices == null)
            {
                return null;
            }
            return SubServices;
        }

        // POST: api/SubServices
        public void Post(SubServices SubServices)
        {
            try
            {
                repository.Add(SubServices);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/SubServices/5
        public void Put(SubServices model)
        {
            try
            {
                SubServices SubServices = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (SubServices != null)
                    {
                        model.Image = model.Image == null ? SubServices.Image : model.Image;
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

        // DELETE: api/SubServices/5
        public void Delete(int id)
        {
            try
            {
                SubServices SubServices = repository.Get(id);
                List<string> images = new List<string>();
                if (SubServices.Image != null)
                {
                    images.AddRange(SubServices.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["SubServicesLocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(SubServices);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
