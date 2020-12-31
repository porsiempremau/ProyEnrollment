using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TypePeriodVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Mounth { get; set; }
        public bool IsActive { get; set; }
        public ICollection<AgreementVM> Agreements { get; set; }
    }
}
