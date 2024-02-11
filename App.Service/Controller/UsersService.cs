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
using System.Net.Http.Headers;

namespace App.Service.Controllers
{
    public class UsersService
    {
        public ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Users> repository;

        public UsersService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Users>(unitOfWork);
        }

        // GET: api/Users/5
        public bool GetUser(string userName, string password)
        {
            Users Users = repository.Find(x=>x.UserName == userName && x.Password == password).FirstOrDefault();
            if (Users == null)
                return false;
            else {
                return true;
            }
            
        }
        // GET: api/Users/5
        public Users LogIn(string userName, string password)
        {
            Users Users = repository.Find(x => x.UserName == userName && x.Password == password).FirstOrDefault();
           return Users;
        }

        // PUT: api/Users/5
        public bool ChangeUserInfo(Users model)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}
