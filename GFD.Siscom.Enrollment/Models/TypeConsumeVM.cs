using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TypeConsumeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AgreementVM> Agreements { get; set; }
        public ICollection<TariffVM> Tariffs { get; set; }
    }
}
