﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class SystemsParametersVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Int16 TypeColumn { get; set; }
        public decimal NumberColumn { get; set; }
        public string TextColumn { get; set; }
        public DateTime DateColumn { get; set; }
    }
}
