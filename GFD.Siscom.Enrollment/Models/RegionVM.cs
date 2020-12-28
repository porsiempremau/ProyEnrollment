using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class RegionVM
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public ICollection<SuburbVM> Suburbs { get; set; }
    }
}
