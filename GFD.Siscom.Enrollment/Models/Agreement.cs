using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class Agreement
    {
        public int id { get; set; }
        public string account { get; set; }
        public string typeAgreement { get; set; }
        public string route { get; set; }
        public string token { get; set; }
        public DateTime accountDate { get; set; }
        public List<ClientVM> clients { get; set; }
        public List<AddressVM> addresses { get; set; }
        public List<AgreementDetailVM> agreementDetails { get; set; }
        public int typeIntakeId { get; set; }
        public TypeIntakeVM typeIntake { get; set; }
        public int typeConsumeId { get; set; }
        public TypeConsumeVM typeConsume { get; set; }
        public List<AgreementDiscountVM> agreementDiscounts { get; set; }
    }
}
