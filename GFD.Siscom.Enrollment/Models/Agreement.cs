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
    }
}
