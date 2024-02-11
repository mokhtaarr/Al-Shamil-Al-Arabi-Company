using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Dal.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App.VM
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public int? CategoryID { get; set; }
        public string Name { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public string ArContent { get; set; }
        public string EnContent { get; set; }

        //[Remote("IsSortExist", "Categoies", ErrorMessage = "User Already Available")]
        [Remote("IsSortExist", "Categoies", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "sortIsExist")]
        [Display(Name = "Sort", ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Required")]
        public int? Sort { get; set; }

        public string Content { get; set; }
        public virtual ICollection<Category> AllCategories { get; set; }
        public virtual ICollection<CategoryCnotent> CategoryCnotent { get; set; }
    }
}
