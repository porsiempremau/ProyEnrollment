using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ClasificationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SuburbVM> Suburbs { get; set; }
    }
}
