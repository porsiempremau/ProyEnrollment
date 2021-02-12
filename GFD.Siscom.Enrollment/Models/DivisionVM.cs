using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class DivisionVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int IdSolution { get; set; }
        public ICollection<ProductVM> Products { get; set; }
        public ICollection<OrderSaleVM> OrderSale { get; set; }
    }
}
