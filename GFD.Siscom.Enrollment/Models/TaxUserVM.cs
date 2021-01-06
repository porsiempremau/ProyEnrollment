using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TaxUserVM
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String RFC { get; set; }
        public String CURP { get; set; }
        public String PhoneNumber { get; set; }
        public String EMail { get; set; }
        public bool IsActive { get; set; }
        public bool IsProvider { get; set; }
        public List<TaxAddressVM> TaxAddresses { get; set; }
    }
}
