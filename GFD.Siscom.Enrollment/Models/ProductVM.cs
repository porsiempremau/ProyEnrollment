using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16 Order { get; set; }
        public int Parent { get; set; }
        public bool HaveTariff { get; set; }
        public bool IsActive { get; set; }
        public bool HaveAccount { get; set; }
        public int DivisionId { get; set; }
        //public Division Division { get; set; }
        public ICollection<TariffProduct> TariffProducts { get; set; }
        public ICollection<ProductParam> ProductParams { get; set; }
    }
}
