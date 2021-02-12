using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class DebtDetailVM
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal OnAccount { get; set; }
        public decimal OnPayment { get; set; }
        public decimal UnitPrice { get; set; }
        public bool HaveTax { get; set; }
        public string CodeConcept { get; set; }
        public string NameConcept { get; set; }
        public decimal Quantity { get; set; }
        public decimal Tax { get; set; }
        public decimal OldValue { get; set; }
        public int NumberPeriod { get; set; }
        public int DebtId { get; set; }
        public DebtVM Debt { get; set; }
    }
}
