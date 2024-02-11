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
    public class FeaturesService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Features> repository;

        public FeaturesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Features>(unitOfWork);
        }

        // GET: api/GetFeaturesForHome
        public Features GetFeaturesForHome(int Language)
        {
            try
            {
                Features Features = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                if (Features == null)
                {
                    return null;
                }
                return Features;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Features
        public FeaturesVM GetFeatures()
        {
            try
            {
                Features Features = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (Features == null)
                {
                    return null;
                }
                return Mapper.Map(Features, new FeaturesVM()); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Features/5
        public Features GetFeaturesById(int id)
        {
            Features Features = repository.Get(id);
            if (Features == null)
            {
                return null;
            }
            return Features;
        }

        // POST: api/Features
        public void Post(Features Features)
        {
            try
            {
                Features.Date = DateTime.Now;
                repository.Add(Features);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/Features/5
        public void Put(Features model)
        {
            try
            {
                Features Features = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (Features != null)
                    {
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

        // DELETE: api/Features/5
        public void Delete(int id)
        {
            try
            {
                Features Features = repository.Get(id);
                repository.Remove(Features);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
