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
    public class SubPortfoliosService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<SubPortfolios> repository;

        public SubPortfoliosService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<SubPortfolios>(unitOfWork);
        }

        
        // GET: api/SubPortfolios
        public List<SubPortfolioVM> GetSubPortfolios()
        {
            try
            {
                List<SubPortfolios> SubPortfolios = repository.GetAll().ToList();
                if (SubPortfolios == null)
                {
                    return null;
                }
                return Mapper.Map(SubPortfolios, new List<SubPortfolioVM>());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Portfolio> GetAllPortfolios()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<Portfolio> Portfolios = db.Portfolio.ToList();
                    if (Portfolios == null)
                    {
                        return null;
                    }
                    return Portfolios;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<PortfolioType> GetAllPortfolioTypes()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<PortfolioType> PortfolioType = db.PortfolioType.ToList();
                    if (PortfolioType == null)
                    {
                        return null;
                    }
                    return PortfolioType;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/SubPortfolios/5
        public SubPortfolios GetSubPortfoliosById(int id)
        {
            SubPortfolios SubPortfolios = repository.Get(id);
            if (SubPortfolios == null)
            {
                return null;
            }
            return SubPortfolios;
        }

        // POST: api/SubPortfolios
        public void Post(SubPortfolios SubPortfolios)
        {
            try
            {
                repository.Add(SubPortfolios);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/SubPortfolios/5
        public void Put(SubPortfolios model)
        {
            try
            {
                SubPortfolios SubPortfolios = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (SubPortfolios != null)
                    {
                        model.Image = model.Image == null ? SubPortfolios.Image : model.Image;
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

        // DELETE: api/SubPortfolios/5
        public void Delete(int id)
        {
            try
            {
                SubPortfolios SubPortfolios = repository.Get(id);
                List<string> images = new List<string>();
                if (SubPortfolios.Image != null)
                {
                    images.AddRange(SubPortfolios.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(SubPortfolios);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
