using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using App.Static.Resources;

namespace App.VM
{
    public class UserSubscribedVM
    {
        [Key]
        public int Id { get; set; }
        [Remote("CheckIfEmailExisting", "ContactUs", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailAlreadyExist")]
        public string Email { get; set; }
    }
}
