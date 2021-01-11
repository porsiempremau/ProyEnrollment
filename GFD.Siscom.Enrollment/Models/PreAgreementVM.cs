using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class PreAgreementVM
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public int TypeIntakeId { get; set; }
        public TypeIntakeVM TypeIntake { get; set; }
        public int TypeServiceId { get; set; }
        public TypeServiceVM TypeService { get; set; }
        public int TypeUseId { get; set; }
        public TypeUseVM TypeUse { get; set; }
        public int TypeClassificationId { get; set; }
        public TypeClassification TypeClassification { get; set; }
        public string ClientName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientSecondLastName { get; set; }
        public string Street { get; set; }
        public string Outdoor { get; set; }
        public string Indoor { get; set; }
        public string Zip { get; set; }
        public string Reference { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public int SuburbsId { get; set; }
        public SuburbVM Suburbs { get; set; }
        public int ServiceId1 { get; set; }
        public int ServiceId2 { get; set; }
        public int ServiceId3 { get; set; }
        public string RegistrationReason { get; set; }
        public string Observation { get; set; }
        public string Status { get; set; }
        public DateTime DateRegistration { get; set; }
        public int OrderWorkId { get; set; }
        public string folio_order_result { get; set; }
        public int agreementId_new { get; set; }

    }
}
