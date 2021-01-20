using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AddressVM
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        public string Zip { get; set; }
        public string Reference { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string TypeAddress { get; set; }
        public int SuburbsId { get; set; }
        public bool IsActive { get; set; }
        public SuburbVM suburbs { get; set; }
    }
}
