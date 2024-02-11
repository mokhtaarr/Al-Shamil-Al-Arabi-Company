using App.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace App.Dal.Model
{
    public class UserSubscribed
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
    }
}
