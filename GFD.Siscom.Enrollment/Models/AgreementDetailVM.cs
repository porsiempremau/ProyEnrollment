﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AgreementDetailVM
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string Register { get; set; }
        public decimal TaxableBase { get; set; }
        public decimal Ground { get; set; }
        public decimal Built { get; set; }
        public DateTime AgreementDetailDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public Int16 Sector { get; set; }
        public string Observation { get; set; }
        public Boolean Manifest { get; set; }
        public string CatastralKey { get; set; }
        public int AgreementId { get; set; }
    }
}
