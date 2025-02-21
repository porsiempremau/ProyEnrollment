﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class DerivativesVM
    {
        public int Id { get; set; }
        public string AccountAgreement { get; set; }
        public string AccountDerivative { get; set; }
        public int IdDerivative { get; set; }
        public bool IsActive { get; set; }
    }
}
