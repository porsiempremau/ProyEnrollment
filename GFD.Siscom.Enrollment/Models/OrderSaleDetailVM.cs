using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class OrderSaleDetailVM
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Unity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool HaveTax { get; set; }
        public string Description { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal OnPayment { get; set; }
        public decimal Tax { get; set; }
        public int idBreachList { get; set; }
        public int OrderSaleId { get; set; }
        public OrderSaleVM OrderSale { get; set; }
    }
}
