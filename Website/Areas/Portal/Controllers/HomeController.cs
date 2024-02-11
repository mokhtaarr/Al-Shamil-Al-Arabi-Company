using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Service.Controllers;
using Website.Controllers;
using App.Dal.Model;
using AutoMapper;
using App.VM;
using App.Static.Enums;
using System.Configuration;

namespace Website.Areas.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public CategoriesService categoriesApi = new CategoriesService();
        public CompanyWordService companyWord = new CompanyWordService();
        public AboutUsService AboutUs = new AboutUsService();
        public ServicesService services = new ServicesService();
        public FeaturesService features = new FeaturesService();
        public PortfolioService portfolio = new PortfolioService();
        public PortfolioTypesService portfolioTypes = new PortfolioTypesService();
        public TeamService team = new TeamService();
        public ContactUsService contact = new ContactUsService();
        public LogosTechnologyService logos = new LogosTechnologyService();

        
        int Language { get; set; }
        
        // GET: Portal/Home
        public ActionResult Index()
        {
            Language = GetLanguage();
            HomeVM homeVM = new HomeVM()
            {
                companyWord = companyWord.GetCompanyWordForHome(Language),
                aboutUs = AboutUs.GetAboutUsForHome(Language),
                contact = contact.GetContactUsForHome(Language),
               
                services = services.GetServicesForHome(Language),
                features = features.GetFeaturesForHome(Language),
                portfolio = portfolio.GetPortfolioForHome(Language),
                PortfolioType = portfolioTypes.GetPortfolioTypesForHome(),
                team = team.GetTeamForHome(Language),
                logosTechnology = logos.GetAll(),
                Language = Language,
                path = ConfigurationManager.AppSettings["LocalPath"],
            };
            return View(homeVM);
        }

        public ActionResult Page(int Id)
        {
            Language = GetLanguage();
            Category category = categoriesApi.GetCategory(Id);
            
            category.CategoryCnotent = category.CategoryCnotent.ToList().Select(x=> new CategoryCnotent { 
                ArContent = Language == (int)LanuageType.AR ? x.ArContent : x.EnContent,
                CategoryID = x.CategoryID,
                Id =x.Id
            }).ToList();

            CategoryVM categoryVM = Mapper.Map(category, new CategoryVM());
            //categoryVM.Content = Language == (int)LanuageType.AR ? categoryVM.ArContent : categoryVM.EnContent;
            categoryVM.Name = Language == (int)LanuageType.AR ? categoryVM.ArName : categoryVM.EnName;
            return View(categoryVM);
        }
    }
}