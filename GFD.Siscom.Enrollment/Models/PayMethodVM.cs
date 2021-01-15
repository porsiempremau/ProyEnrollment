using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PayMethodVM
    {
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string Code { get; set; }       
        public bool IsActive { get; set; }
        //public ICollection<PaymentVM> Payments { get; set; }
    }
}
