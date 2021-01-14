using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class NotificationDetailVM
    {     
        public int Id { get; set; }        
        public double Amount { get; set; }       
        public bool HaveTax { get; set; }        
        public string CodeConcept { get; set; }       
        public string NameConcept { get; set; }
        public int NotificationId { get; set; }
        public NotificationVM Notification { get; set; }
    }
}
