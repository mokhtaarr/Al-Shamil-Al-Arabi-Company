using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dal.Model
{
    public class Portfolio
    {
        public Portfolio()
        {
            SubPortfolios = new HashSet<SubPortfolios>();
        }

        [Key]
        public int Id { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<SubPortfolios> SubPortfolios { get; set; }
    }
}
