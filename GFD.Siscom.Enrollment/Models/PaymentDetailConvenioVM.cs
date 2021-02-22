using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PaymentDetailConvenioVM
    {
        public decimal amount { get; set; }
        public string description { get; set; }
        public decimal iva { get; set; }
        public decimal onAccount { get; set; }
        public string paymentDay { get; set; }
        public int paymentNumber { get; set; }
        public string releaseDate { get; set; }
        public string releasePeriod { get; set; }
    }
}
