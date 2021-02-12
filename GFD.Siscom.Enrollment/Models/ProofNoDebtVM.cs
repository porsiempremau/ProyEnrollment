using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ProofNoDebtVM
    {
        public int Id { get; set; }
        public int Folio { get; set; }
        public DateTime Expedition_date { get; set; }
        public string UserId { get; set; }
        public DateTime Expiration_date { get; set; }
        public int AgreementId { get; set; }
        public Agreement Agreement { get; set; }
    }
}
