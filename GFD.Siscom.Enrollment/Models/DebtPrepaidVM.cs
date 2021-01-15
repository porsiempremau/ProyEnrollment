using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class DebtPrepaidVM
    {
        public int Id { get; set; }
        
        public string CodeConcept { get; set; }
       
        public string NameConcept { get; set; }
       
        public decimal OriginalAmount { get; set; }
        
        public decimal PaymentAmount { get; set; }
       
        public int DebtId { get; set; }

        public int PrepaidDetailId { get; set; }
        public PrepaidDetailVM PrepaidDetail { get; set; }
    }
}
