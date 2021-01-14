using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class TaxReceiptVM
    {
        public int Id { get; set; }        
        public DateTime TaxReceiptDate { get; set; }        
        public string XML { get; set; }        
        public string FielXML { get; set; }        
        public String RFC { get; set; }        
        public string Type { get; set; }        
        public string Status { get; set; }        
        public string IdXmlFacturama { get; set; }        
        public string UsoCFDI { get; set; }       
        public byte[] PDFInvoce { get; set; }
        public string UserId { get; set; }
        //public ApplicationUser User { get; set; }
        public int PaymentId { get; set; }
        public PaymentVM Payment { get; set; }
        //public ICollection<TaxReceiptCancel> TaxReceiptCancels { get; set; }
    }
}
