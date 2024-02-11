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
    public class CategoryCnotent
    {
        public CategoryCnotent()
        {
        }

        [Key]
        public int Id { get; set; }
        
        [StringLength(int.MaxValue)]
        [AllowHtml]
        public string ArContent { get; set; }
       
        [StringLength(int.MaxValue)]
        [AllowHtml]
        public string EnContent { get; set; }

        [StringLength(int.MaxValue)]
        public string Images { get; set; }

        [ForeignKey("Categories")]
        public int CategoryID { get; set; }
        public virtual Category Categories { get; set; }
    }
}
