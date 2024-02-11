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
    public class TeamMembersService
    {
        public ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<TeamMembers> repository;

        public TeamMembersService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<TeamMembers>(unitOfWork);
        }

        // GET: api/TeamMembers
        public List<TeamMembersVM> GetTeamMembers()
        {
            try
            {
                List<TeamMembers> TeamMembers = repository.GetAll().ToList();
                if (TeamMembers == null)
                {
                    return null;
                }
                return Mapper.Map(TeamMembers, new List<TeamMembersVM>());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Team> GetAllTeam()
        {
            try
            {
                using (db = new ApplicationDbContext())
                {
                    List<Team> Features = db.Team.ToList();
                    if (Features == null)
                    {
                        return null;
                    }
                    return Features;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/TeamMembers/5
        public TeamMembers GetTeamMembersById(int id)
        {
            TeamMembers TeamMembers = repository.Get(id);
            if (TeamMembers == null)
            {
                return null;
            }
            return TeamMembers;
        }

        // POST: api/TeamMembers
        public void Post(TeamMembers TeamMembers)
        {
            try
            {
                repository.Add(TeamMembers);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/TeamMembers/5
        public void Put(TeamMembers model)
        {
            try
            {
                TeamMembers TeamMembers = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (TeamMembers != null)
                    {
                        model.Image = model.Image == null ? TeamMembers.Image : model.Image;
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

        // DELETE: api/TeamMembers/5
        public void Delete(int id)
        {
            try
            {
                TeamMembers TeamMembers = repository.Get(id);
                List<string> images = new List<string>();
                if (TeamMembers.Image != null)
                {
                    images.AddRange(TeamMembers.Image.Split(',').ToList());
                    foreach (var item in images)
                    {
                        string str = Path.Combine(HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["LocalPath"] + "/"), item);
                        if (System.IO.File.Exists(str))
                        {
                            System.IO.File.Delete(str);
                        }
                    }
                }
                repository.Remove(TeamMembers);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
