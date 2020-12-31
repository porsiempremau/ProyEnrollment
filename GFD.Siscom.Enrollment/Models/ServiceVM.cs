using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ServiceVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Order { get; set; }
        public bool IsService { get; set; }
        public bool HaveTax { get; set; }
        public bool InAgreement { get; set; }
        public bool IsActive { get; set; }
    }
}
