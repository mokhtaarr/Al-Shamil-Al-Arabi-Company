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
    public class CompanyWordService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<CompanyWord> repository;

        public CompanyWordService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<CompanyWord>(unitOfWork);
        }


        // GET: api/GetCompanyWordForHome
        public CompanyWordVM GetCompanyWordForHome(int Language)
        {
            try
            {
                CompanyWord companyWord = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                CompanyWordVM companyWordVM = new CompanyWordVM();
                if (companyWord == null)
                {
                    return null;
                }
                if (companyWord != null)
                {
                    companyWordVM = new CompanyWordVM() { 
                        Titel = Language == (int)LanuageType.AR ? companyWord.ArTitel : companyWord.EnTitel,
                        Description = Language == (int)LanuageType.AR ? companyWord.ArDescription : companyWord.EnDescription,
                        Image = companyWord.Image,
                    };
                }
                return companyWordVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/CompanyWord
        public CompanyWordVM GetCompanyWord()
        {
            try
            {
                CompanyWord companyWord = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (companyWord == null)
                {
                    return null;
                }
                return Mapper.Map(companyWord, new CompanyWordVM());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/CompanyWord/5
        public CompanyWord GetCompanyWordById(int id)
        {
            CompanyWord companyWord = repository.Get(id);
            if (companyWord == null)
            {
                return null;
            }
            return companyWord;
        }

        // POST: api/CompanyWord
        public void Post(CompanyWord CompanyWord)
        {
            try
            {
                CompanyWord.Date = DateTime.Now;
                repository.Add(CompanyWord);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/CompanyWord/5
        public void Put(CompanyWord model)
        {
            try
            {
                CompanyWord companyWord = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (companyWord != null)
                    {
                        model.Image = model.Image == null ? companyWord.Image : model.Image;
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

        // DELETE: api/CompanyWord/5
        public void Delete(int id)
        {
            try
            {
                CompanyWord companyWord = repository.Get(id);
                List<string> images = new List<string>();
                if (companyWord.Image != null)
                {
                    images.AddRange(companyWord.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/" + 
                            ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(companyWord);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
