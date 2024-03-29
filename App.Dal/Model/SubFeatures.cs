﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Dal.Model
{
    public class SubFeatures
    {
        public SubFeatures()
        {
        }

        public int Id { get; set; }
        public string ArTitel { get; set; }
        public string EnTitel { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public string Image { get; set; }

        [ForeignKey("Features")]
        public int FeatureID { get; set; }
        public virtual Features Features { get; set; }
    }
}
