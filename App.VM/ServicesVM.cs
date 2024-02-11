﻿using App.Static.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VM
{
    public class ServicesVM
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ArDescription { get; set; }
        public string EnDescription { get; set; }
        public DateTime Date { get; set; }
    }
}
