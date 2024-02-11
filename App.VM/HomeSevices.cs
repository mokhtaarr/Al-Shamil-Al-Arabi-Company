using App.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VM
{
    public class HomeSevices
    {
        public string Description { get; set; }
        public virtual ICollection<SubServices> SubServices { get; set; }
    }
}
