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
    public class ServicesService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Services> repository;

        public ServicesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Services>(unitOfWork);
        }

        // GET: api/GetServicesForHome
        public Services GetServicesForHome(int Language)
        {
            try
            {
                Services Services = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                if (Services == null)
                {
                    return null;
                }
                return Services;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Services
        public ServicesVM GetServices()
        {
            try
            {
                Services Services = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (Services == null)
                {
                    return null;
                }
                return Mapper.Map(Services, new ServicesVM());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Services/5
        public Services GetServicesById(int id)
        {
            Services Services = repository.Get(id);
            if (Services == null)
            {
                return null;
            }
            return Services;
        }

        // POST: api/Services
        public void Post(Services Services)
        {
            try
            {
                Services.Date = DateTime.Now;
                repository.Add(Services);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/Services/5
        public void Put(Services model)
        {
            try
            {
                Services Services = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (Services != null)
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

        // DELETE: api/Services/5
        public void Delete(int id)
        {
            try
            {
                Services Services = repository.Get(id);
                repository.Remove(Services);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
