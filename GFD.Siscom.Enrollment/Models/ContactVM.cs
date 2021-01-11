using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ContactVM
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeNumber { get; set; }
        public int IsActive { get; set; }
        public int ClientId { get; set; }
    }
}
