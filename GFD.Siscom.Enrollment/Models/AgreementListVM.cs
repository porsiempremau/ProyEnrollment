using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AgreementListVM
    {
        public int agreementId { get; set; }
        public string account { get; set; }
        public string nombre { get; set; }
        public decimal taxableBase { get; set; }
        public int idClient { get; set; }
        public string rfc { get; set; }
        public string address { get; set; }
        public bool withDiscount { get; set; }
        public int idStatus { get; set; }
        public string status { get; set; }
        public string type { get; set; }
        public int numDerivades { get; set; }
        public decimal debit { get; set; }
        public string token { get; set; }
        //public DateTime endDate { get; set; }
        public string nameDiscount { get; set; }
        public bool isActiveDiscount { get; set; }
    }
}
