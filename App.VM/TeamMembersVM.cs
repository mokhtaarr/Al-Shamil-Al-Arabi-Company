using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VM
{
    public class TeamMembersVM
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string ArJob { get; set; }
        public string EnJob { get; set; }
        public string Image { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedin { get; set; }
        public int TeamID { get; set; }
    }
}
