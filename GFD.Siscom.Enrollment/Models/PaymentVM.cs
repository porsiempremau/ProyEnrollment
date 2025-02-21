﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PaymentVM
    {
        public int Id { get; set; }        
        public DateTime PaymentDate { get; set; }       
        public string BranchOffice { get; set; }        
        public decimal Subtotal { get; set; }        
        public string PercentageTax { get; set; }        
        public decimal Tax { get; set; }        
        public decimal Rounding { get; set; }        
        public decimal Total { get; set; }        
        public string AuthorizationOriginPayment { get; set; }       
        public string NumberBank { get; set; }        
        public string AccountNumber { get; set; }        
        public string TransactionFolio { get; set; }        
        public int AgreementId { get; set; }        
        public string Status { get; set; }        
        public string Type { get; set; }        
        public string PayMethodNumber { get; set; }       
        public bool HaveTaxReceipt { get; set; }        
        public string Account { get; set; }        
        public int OrderSaleId { get; set; }        
        public string ObservationInvoice { get; set; }        
        public int TerminalUserId { get; set; }        
        public string ImpressionSheet { get; set; }       
        public int OriginPaymentId { get; set; }
        //public OriginPayment OriginPayment { get; set; }       
        public int ExternalOriginPaymentId { get; set; }
        //public ExternalOriginPayment ExternalOriginPayment { get; set; }

        //[ForeignKey("PayMethod")]
        public int PayMethodId { get; set; }
        public PayMethodVM PayMethod { get; set; }       
        public decimal CashPayment { get; set; }       
        public decimal CardPayment { get; set; }      
        public decimal BankDraftPayment { get; set; }       
        public decimal TansferencePayment { get; set; }
        public ICollection<PaymentDetailVM> PaymentDetails { get; set; }
        public ICollection<TaxReceiptVM> TaxReceipts { get; set; }
        //public ICollection<DetailOfPaymentMethods> DetailOfPaymentMethods { get; set; }
    }
}
