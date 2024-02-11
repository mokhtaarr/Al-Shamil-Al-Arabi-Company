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
    public class Category
    {
        public Category()
        {
        }

        [Key]
        public int Id { get; set; }
        public string ArName { get; set; }
        public string EnName { get; set; }
        public int? Sort { get; set; }

        [ForeignKey("Categories")]
        public int? CategoryID { get; set; }
        public virtual Category Categories { get; set; }
        public virtual ICollection<Category> AllCategories { get; set; }
        public virtual ICollection<CategoryCnotent> CategoryCnotent { get; set; }
    }
}
