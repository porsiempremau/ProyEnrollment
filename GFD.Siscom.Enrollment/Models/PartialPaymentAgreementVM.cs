﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PartialPaymentAgreementVM
    {
        public int idPartialPayment { get; set; }
        public string folio { get; set; }
        public string partialPaymentDate { get; set; }
        public decimal amount { get; set; }
        public int numberPayments { get; set; }
        public string description { get; set; }
        public string expiration_date { get; set; }
        public int AgreementId { get; set; }
        public string Account { get; set; }
    }
}
