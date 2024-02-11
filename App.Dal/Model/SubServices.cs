using App.Dal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dal.Model
{
    public class SubServices
    {
        public SubServices()
        {
        }

        [Key]
        public int Id { get; set; }
        public string ArTitel { get; set; }
        public string EnTitel { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Image { get; set; }

        [ForeignKey("Services")]
        public int ServiceID { get; set; }
        public virtual Services Services { get; set; }
    }
}
