using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dal.Model
{
    public class Team
    {
        public Team()
        {
            TeamMembers = new HashSet<TeamMembers>();
        }

        [Key]
        public int Id { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<TeamMembers> TeamMembers { get; set; }
    }
}
