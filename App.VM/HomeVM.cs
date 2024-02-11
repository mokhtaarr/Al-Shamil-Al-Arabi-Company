using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Dal.Model;

namespace App.VM
{
    public class HomeVM
    {
        public CompanyWordVM companyWord { get; set; }
        public AboutUsVM aboutUs{ get; set; }
        public ContactUsVM contact { get; set; }
        public Services services { get; set; }
        public Features features { get; set; }
        public Portfolio portfolio { get; set; }
        public List<PortfolioType> PortfolioType { get; set; }
        public List<LogosTechnologyVM> logosTechnology { get; set; }
        public Team team { get; set; }
        public string path { get; set; }
        public int Language { get; set; }
    }
}
