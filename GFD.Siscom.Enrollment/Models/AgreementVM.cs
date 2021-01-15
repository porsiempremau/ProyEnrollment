using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AgreementVM
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Route { get; set; }
        //public int? Derivatives { get; set; }
        public int TypeServiceId { get; set; }
        public int TypeUseId { get; set; }
        public int TypeConsumeId { get; set; }
        public int TypeRegimeId { get; set; }
        public int TypePeriodId { get; set; }
        public int TypeCommertialBusinessId { get; set; }
        public int TypeStateServiceId { get; set; }
        public int TypeIntakeId { get; set; }
        public int TypeClasificationId { get; set; }
        public int DiameterId { get; set; }
        public string TypeAgreement { get; set; }
        public int AgreementPrincipalId { get; set; }
        public string UserId { get; set; }
        public string Observations { get; set; }
        public List<int> ServicesId { get; set; }
        public List<AddressVM> Adresses { get; set; }
        public List<ClientVM> Clients { get; set; }
        public List<AgreementDetailVM> AgreementDetails { get; set; }

        //public OrderWork OrderWork { get; set; }
        public ICollection<AccountStatusInFileVM> AccountStatusInFiles { get; set; }
        public ICollection<DebtVM> Debts { get; set; }

        //public ICollection<DerivativeVM> Derivatives { get; set; }
    }
}
