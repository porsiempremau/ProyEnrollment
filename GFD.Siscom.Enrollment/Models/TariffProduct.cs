using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TariffProduct
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public bool HaveTax { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public int IsActive { get; set; }
        public decimal Percentage { get; set; }
        public Int16 TimesFactor { get; set; }
        public bool IsVariable { get; set; }
        public string Type { get; set; }
        public string DescriptionType { get; set; }
        public int ProductId { get; set; }
        public ProductVM Product { get; set; }
    }
}
