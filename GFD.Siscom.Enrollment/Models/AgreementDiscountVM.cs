using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AgreementDiscountVM
    {
        public int idDiscount { get; set; }
        public DiscountsVM discount { get; set; }
        public int idAgreement { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isActive { get; set; }
    }
}
