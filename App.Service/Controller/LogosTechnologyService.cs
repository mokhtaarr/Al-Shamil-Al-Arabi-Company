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
    public class LogosTechnologyService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<LogosTechnology> repository;

        public LogosTechnologyService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<LogosTechnology>(unitOfWork);
        }

        // GET: api/LogosTechnology
        public List<LogosTechnologyVM> GetAll()
        {
            try
            {
                List<LogosTechnology> LogosTechnology = repository.GetAll().OrderByDescending(x=>x.Sort).ToList();
                if (LogosTechnology == null)
                {
                    return null;
                }
                return Mapper.Map(LogosTechnology, new List<LogosTechnologyVM>());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/LogosTechnology/5
        public LogosTechnology GetLogosTechnologyById(int id)
        {
            LogosTechnology LogosTechnology = repository.Get(id);
            if (LogosTechnology == null)
            {
                return null;
            }
            return LogosTechnology;
        }

        // POST: api/LogosTechnology
        public void Post(LogosTechnology LogosTechnology)
        {
            try
            {
                repository.Add(LogosTechnology);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/LogosTechnology/5
        public void Put(LogosTechnology model)
        {
            try
            {
                //LogosTechnology LogosTechnology = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    //if (LogosTechnology != null)
                    //{
                    //}
                }
            }
            catch (Exception dd)
            {
                unitOfWork.Rollback();
            }
        }

        // DELETE: api/LogosTechnology/5
        public void Delete(int id)
        {
            try
            {
                LogosTechnology LogosTechnology = repository.Get(id);
                repository.Remove(LogosTechnology);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }

        public bool CheckIfsortExisting(int Id, string Sort)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    int _Sort = Sort == "" ? 0 : int.Parse(Sort);
                    LogosTechnology Model = repository.Find(u => u.Sort == _Sort && u.Id != Id).FirstOrDefault();
                    return Model == null ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }

        public int? GetLastSort()
        {
            using (db = new ApplicationDbContext())
            {
                int? sort = repository.GetAll().Select(x => x.Sort).OrderByDescending(x => x)?.FirstOrDefault();
                return sort;
            }
        }
    }
}
