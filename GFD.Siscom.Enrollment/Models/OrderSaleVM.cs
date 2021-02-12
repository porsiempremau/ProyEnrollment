using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class OrderSaleVM
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public DateTime DateOrder { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public Int16 Year { get; set; }
        public Int16 Period { get; set; }
        public string Type { get; set; }
        public string DescriptionType { get; set; }
        public string Status { get; set; }
        public string DescriptionStatus { get; set; }
        public string Observation { get; set; }
        public int IdOrigin { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserName { get; set; }
        public int DivisionId { get; set; }
        public DivisionVM Division { get; set; }
        public int TaxUserId { get; set; }
        public TaxUserVM TaxUser { get; set; }
        public ICollection<OrderSaleDetailVM> OrderSaleDetails { get; set; }
        public ICollection<OrderSaleDiscountVM> OrderSaleDiscounts { get; set; }
        public ICollection<OrderSaleStatusVM> OrderSaleStatuses { get; set; }
    }
}
