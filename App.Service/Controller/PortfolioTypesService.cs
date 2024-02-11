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
    public class PortfolioTypesService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<PortfolioType> repository;

        public PortfolioTypesService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<PortfolioType>(unitOfWork);
        }

        // GET: api/GetPortfolioTypesForHome
        public List<PortfolioType> GetPortfolioTypesForHome()
        {
            try
            {
                List<PortfolioType> PortfolioType = repository.GetAll().ToList();
                if (PortfolioType == null)
                {
                    return null;
                }
                return PortfolioType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/PortfolioType
        public List<PortfolioTypesVM> GetPortfolioTypes()
        {
            try
            {
                List<PortfolioType> PortfolioType = repository.GetAll().ToList();
                if (PortfolioType == null)
                {
                    return null;
                }
                return Mapper.Map(PortfolioType, new List<PortfolioTypesVM>()); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/PortfolioType/5
        public PortfolioType GetPortfolioTypesById(int id)
        {
            PortfolioType PortfolioType = repository.Get(id);
            if (PortfolioType == null)
            {
                return null;
            }
            return PortfolioType;
        }

        // POST: api/PortfolioType
        public void Post(PortfolioType PortfolioType)
        {
            try
            {
                repository.Add(PortfolioType);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/PortfolioType/5
        public void Put(PortfolioType model)
        {
            try
            {
                PortfolioType PortfolioType = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (PortfolioType != null)
                    {
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

        // DELETE: api/PortfolioType/5
        public void Delete(int id)
        {
            try
            {
                PortfolioType PortfolioType = repository.Get(id);
                repository.Remove(PortfolioType);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
