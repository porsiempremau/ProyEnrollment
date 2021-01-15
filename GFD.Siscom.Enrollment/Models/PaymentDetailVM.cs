using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PaymentDetailVM
    {
        public int Id { get; set; }        
        public string CodeConcept { get; set; }        
        public string AccountNumber { get; set; }        
        public string UnitMeasurement { get; set; }       
        public string Description { get; set; }        
        public decimal Amount { get; set; }        
        public int DebtId { get; set; }        
        public DebtVM Debt { get; set; }        
        public int PrepaidId { get; set; }        
        public PrepaidVM Prepaid { get; set; }        
        public int OrderSaleId { get; set; }
        
        //public OrderSaleVM OrderSale { get; set; }
       
        public bool HaveTax { get; set; }
       
        public decimal Tax { get; set; }
        
        public string Type { get; set; }

        public int PaymentId { get; set; }
        //public PaymentVM Payment { get; set; }
    }
}
