using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VM
{
    public class CompanyWordVM
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string ArTitel { get; set; }
        public string EnTitel { get; set; }
        public string Description { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Image { get; set; }
        public DateTime Date { get; set; }
    }
}
