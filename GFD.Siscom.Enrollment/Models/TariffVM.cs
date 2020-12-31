using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TariffVM
    {
        public int Id { get; set; }
        public string Concept { get; set; }
        public string AccountNumber { get; set; }
        public bool HaveTax { get; set; }
        public Int16 Percentage { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UntilDate { get; set; }
        public int IsActive { get; set; }
        public decimal StartConsume { get; set; }
        public decimal EndConsume { get; set; }
        public bool HaveConsume { get; set; }
        public int ServiceId { get; set; }
        public ServiceVM Service { get; set; }
        public int TypeIntakeId { get; set; }
        public TypeIntakeVM TypeIntake { get; set; }
        public int TypeConsumeId { get; set; }
        public TypeConsumeVM TypeConsume { get; set; }
    }
}
