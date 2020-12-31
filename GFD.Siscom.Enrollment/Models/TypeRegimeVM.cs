using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TypeRegimeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IntakeAcronym { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AgreementVM> Agreements { get; set; }
        public ICollection<PreAgreementVM> PreAgreements { get; set; }
    }
}
