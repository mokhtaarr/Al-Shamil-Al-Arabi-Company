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
    public class TeamService
    {
        public ApplicationDbContext db;
        private UnitOfWork<ApplicationDbContext> unitOfWork;
        private IRepository<Team> repository;

        public TeamService()
        {
            unitOfWork = new UnitOfWork<ApplicationDbContext>();
            repository = new Repository<Team>(unitOfWork);
        }

        // GET: api/GetTeamForHome
        public Team GetTeamForHome(int Language)
        {
            try
            {
                Team Team = repository.GetAll().OrderByDescending(x => x.Date).FirstOrDefault();
                if (Team == null)
                {
                    return null;
                }
                return Team;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Team
        public TeamVM GetTeam()
        {
            try
            {
                Team Team = repository.GetAll().OrderByDescending(x=>x.Date).FirstOrDefault();
                if (Team == null)
                {
                    return null;
                }
                return Mapper.Map(Team, new TeamVM()); ;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Team/5
        public Team GetTeamById(int id)
        {
            Team Team = repository.Get(id);
            if (Team == null)
            {
                return null;
            }
            return Team;
        }

        // POST: api/Team
        public void Post(Team Team)
        {
            try
            {
                Team.Date = DateTime.Now;
                repository.Add(Team);
                unitOfWork.Save();
            }
            catch (Exception ex)
            {
                unitOfWork.Rollback();
            }
        }

        // PUT: api/Team/5
        public void Put(Team model)
        {
            try
            {
                Team Team = repository.Get(model.Id);
                using (db = new ApplicationDbContext())
                {
                    if (Team != null)
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

        // DELETE: api/Team/5
        public void Delete(int id)
        {
            try
            {
                Team Team = repository.Get(id);
                repository.Remove(Team);
                unitOfWork.Save();
            }
            catch
            {
                unitOfWork.Rollback();
            }
        }
    }
}
