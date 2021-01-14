using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PrepaidDetailVM
    {        
        public int Id { get; set; }
       
        public DateTime PrepaidDetailDate { get; set; }
        
        public decimal Amount { get; set; }
        
        public string Status { get; set; }

        public int PrepaidId { get; set; }
        public PrepaidVM Prepaid { get; set; }

        public ICollection<DebtPrepaidVM> DebtPrepaids { get; set; }
    }
}
