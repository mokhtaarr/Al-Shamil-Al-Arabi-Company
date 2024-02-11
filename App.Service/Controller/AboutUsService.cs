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
    public class AboutUsService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<AboutUs> repository;

        public AboutUsService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<AboutUs>(unitOfWork);
        }

        // GET: api/GetAboutUsForHome
        public AboutUsVM GetAboutUsForHome(int Language)
        {
            try
            {
                AboutUs AboutUs = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                AboutUsVM AboutUsVM = new AboutUsVM();
                if (AboutUs == null)
                {
                    return null;
                }
                if (AboutUs != null)
                {
                    AboutUsVM = new AboutUsVM() { 
                        Titel = Language == (int)LanuageType.AR ? AboutUs.ArTitel : AboutUs.EnTitel,
                        Description = Language == (int)LanuageType.AR ? AboutUs.ArDescription : AboutUs.EnDescription,
                        Image = AboutUs.Image,
                    };
                }
                return AboutUsVM;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET: api/AboutUs
        public AboutUsVM GetAboutUs()
        {
            try
            {
                AboutUs AboutUs = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (AboutUs == null)
                {
                    return null;
                }
                return Mapper.Map(AboutUs, new AboutUsVM());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/AboutUs/5
        public AboutUs GetAboutUsById(int id)
        {
            AboutUs AboutUs = repository.Get(id);
            if (AboutUs == null)
            {
                return null;
            }
            return AboutUs;
        }

        // POST: api/AboutUs
        public void Post(AboutUs AboutUs)
        {
            try
            {
                AboutUs.Date = DateTime.Now;
                repository.Add(AboutUs);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/AboutUs/5
        public void Put(AboutUs model)
        {
            try
            {
                AboutUs AboutUs = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (AboutUs != null)
                    {
                        model.Image = model.Image == null ? AboutUs.Image : model.Image;
                        model.Date = DateTime.Now;
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

        // DELETE: api/AboutUs/5
        public void Delete(int id)
        {
            try
            {
                AboutUs AboutUs = repository.Get(id);
                List<string> images = new List<string>();
                if (AboutUs.Image != null)
                {
                    images.AddRange(AboutUs.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(AboutUs);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
