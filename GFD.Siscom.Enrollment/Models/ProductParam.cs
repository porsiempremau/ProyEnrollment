using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ProductParam
    {
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public string UnitMeasurement { get; set; }
        public bool IsActive { get; set; }
        public int ProductId { get; set; }
        public ProductVM Product { get; set; }
    }
}
