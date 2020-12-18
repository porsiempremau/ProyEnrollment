using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class LoginVM
    {
        public string User { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public List<string> RolName { get; set; }
        public int Divition { get; set; }
        public bool CanStamp { get; set; }
        public string Serial { get; set; }
        public string Email { get; set; }
    }
}
