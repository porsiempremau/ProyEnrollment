using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PrepaidVM
    {        
        public int Id { get; set; }
        
        public DateTime PrepaidDate { get; set; }
        
        public decimal Amount { get; set; }
        
        public decimal Accredited { get; set; }
        
        public string Status { get; set; }
        
        public string StatusDescription { get; set; }
       
        public string Type { get; set; }
        
        public string TypeDescription { get; set; }
        
        public Int16 Percentage { get; set; }

        public int AgreementId { get; set; }
        public AgreementVM Agreement { get; set; }

        public ICollection<PrepaidDetailVM> PrepaidDetails { get; set; }
    }
}
