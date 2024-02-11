using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dal.Model
{
    public class ContactUs
    {
        public ContactUs()
        {
        }

        [Key]
        public int Id { get; set; }
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
    }
}
