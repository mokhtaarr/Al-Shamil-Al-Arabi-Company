using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.VM
{
    public class LogosTechnologyVM
    {
        public int Id { get; set; }
        public string ArTitel { get; set; }
        public string EnTitel { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Image { get; set; }

        [Remote("CheckIfsortExisting", "LogosTechnology", "Admin", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "sortIsExist")]
        public int Sort { get; set; }
    }
}
