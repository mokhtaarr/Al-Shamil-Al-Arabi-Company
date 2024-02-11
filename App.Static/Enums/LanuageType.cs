using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Static.Enums
{
    public enum LanuageType
    {
        [Display(Name = "AR", ResourceType = typeof(Resources.Resource))]
        AR = 1,
        EN = 2,
    }
}
