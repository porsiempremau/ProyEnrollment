using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class OrderSaleDiscountVM
    {
        public int Id { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public Int16 DiscountPercentage { get; set; }
        public int OrderSaleDetailId { get; set; }
        public int OrderSaleId { get; set; }
        public OrderSaleVM OrderSale { get; set; }
    }
}
