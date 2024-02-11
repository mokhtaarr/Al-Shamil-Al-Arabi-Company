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
    public class PortfolioService
    {
        public ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Portfolio> repository;

        public PortfolioService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Portfolio>(unitOfWork);
        }

        // GET: api/GetPortfolioForHome
        public Portfolio GetPortfolioForHome(int Language)
        {
            try
            {
                Portfolio Portfolio = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                if (Portfolio == null)
                {
                    return null;
                }
                return Portfolio;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Portfolio
        public PortfolioVM GetPortfolio()
        {
            try
            {
                Portfolio Portfolio = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (Portfolio == null)
                {
                    return null;
                }
                return Mapper.Map(Portfolio, new PortfolioVM()); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Portfolio/5
        public Portfolio GetPortfolioById(int id)
        {
            Portfolio Portfolio = repository.Get(id);
            if (Portfolio == null)
            {
                return null;
            }
            return Portfolio;
        }

        // POST: api/Portfolio
        public void Post(Portfolio Portfolio)
        {
            try
            {
                Portfolio.Date = DateTime.Now;
                repository.Add(Portfolio);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/Portfolio/5
        public void Put(Portfolio model)
        {
            try
            {
                Portfolio Portfolio = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (Portfolio != null)
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

        // DELETE: api/Portfolio/5
        public void Delete(int id)
        {
            try
            {
                Portfolio Portfolio = repository.Get(id);
                repository.Remove(Portfolio);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
