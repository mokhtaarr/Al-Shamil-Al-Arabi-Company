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
    public class ContactUsService
    {
        private ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<ContactUs> repository;

        public ContactUsService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<ContactUs>(unitOfWork);
        }

        // GET: api/GetContactUsForHome
        public ContactUsVM GetContactUsForHome(int Language)
        {
            try
            {
                ContactUs ContactUs = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                ContactUsVM ContactUsVM = new ContactUsVM();
                if (ContactUs == null)
                {
                    return null;
                }
                if (ContactUs != null)
                {
                    ContactUsVM = new ContactUsVM() { 
                        Id = ContactUs.Id,
                        Description = Language == (int)LanuageType.AR ? ContactUs.ArDescription : ContactUs.EnDescription,
                        Addres = ContactUs.Addres,
                        Phone = ContactUs.Phone,
                        Email = ContactUs.Email,
                        twitter = ContactUs.twitter,
                        facebook = ContactUs.facebook,
                        linkedin = ContactUs.linkedin,
                        skype = ContactUs.skype,
                        instagram = ContactUs.instagram,
                        Image = ContactUs.Image,
                    };
                }
                return ContactUsVM;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/ContactUs
        public ContactUsVM GetContactUs()
        {
            try
            {
                ContactUs ContactUs = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (ContactUs == null)
                {
                    return null;
                }
                return Mapper.Map(ContactUs, new ContactUsVM()); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/ContactUs/5
        public ContactUs GetContactUsById(int id)
        {
            ContactUs ContactUs = repository.Get(id);
            if (ContactUs == null)
            {
                return null;
            }
            return ContactUs;
        }

        // POST: api/ContactUs
        public void Post(ContactUs ContactUs)
        {
            try
            {
                ContactUs.Date = DateTime.Now;
                repository.Add(ContactUs);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/ContactUs/5
        public void Put(ContactUs model)
        {
            try
            {
                ContactUs ContactUs = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (ContactUs != null)
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

        // DELETE: api/ContactUs/5
        public void Delete(int id)
        {
            try
            {
                ContactUs ContactUs = repository.Get(id);
                repository.Remove(ContactUs);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }

        public bool Subscribe(string email)
        {
            try
            {
                bool check = CheckIfEmailExisting(email);
                using (db = new ApplicationDbContext())
                {
                    if (check)
                    {
                        UserSubscribed subscribed = new UserSubscribed() { Email = email };
                        db.UserSubscribed.Add(subscribed);
                        db.SaveChanges();
                    }
                    return true;
                }
            }
            catch(Exception x)
            {
                return false;
            }
        }
        public bool CheckIfEmailExisting(string email)
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    UserSubscribed subscribed = db.UserSubscribed.FirstOrDefault(x=>x.Email == email);
                    return subscribed == null ? true : false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
