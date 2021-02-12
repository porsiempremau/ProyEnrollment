using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class OrderSaleStatusVM
    {
        public int Id { get; set; }
        public string id_status { get; set; }
        public DateTime DebtStatusDate { get; set; }
        public string User { get; set; }
        public int OrderSaleId { get; set; }
        public OrderSaleVM OrderSale { get; set; }
    }
}
