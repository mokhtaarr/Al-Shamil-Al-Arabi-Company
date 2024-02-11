using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.VM
{
    public class ContactUsVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Image { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }

        public string twitter { get; set; }
        public string facebook { get; set; }
        public string instagram { get; set; }
        public string linkedin { get; set; }
        public string skype { get; set; }
        public DateTime Date { get; set; }


        [Remote("CheckIfEmailExisting", "ContactUs", "Admin", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailAlreadyExist")]
        public string EmailSubscribed { get; set; }
    }
}
