using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class AgreementDataVM
    {
        public List<TypeServiceVM> TypeService { get; set; }
        public List<TypeUseVM> TypeUse { get; set; }
        public List<TypeConsumeVM> TypeConsume { get; set; }
        public List<TypeRegimeVM> TypeRegime { get; set; }
        public List<TypePeriodVM> TypePeriod { get; set; }
        public List<TypeCommercialBusinessVM> TypeCommertialBusiness { get; set; }
        public List<TypeStateServiceVM> TypeStateService { get; set; }
        public List<TypeIntakeVM> TypeIntake { get; set; }
        public List<DiameterVM> Diameter { get; set; }
        public List<TypeClassification> TypeClassifications { get; set; }
        public List<TypeClient> TypeClients { get; set; }
        public List<TypeAddress> TypeAddresses { get; set; }
        public List<TypeContact> TypeContacts { get; set; }
        public List<ServiceVM> Services { get; set; }
        public List<TypeAgreemnet> TypeAgreemnets { get; set; }
        public List<TypeDiscount> TypeDescounts { get; set; }
        public List<TypeDebt> TypeDebts { get; set; }
        public List<TypeFile> TypeFile { get; set; }
    }

    public class TypeClient
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeAddress
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeContact
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeAgreemnet
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeDiscount
    {
        public int IdType { get; set; }
        public string Description { get; set; }
        public Int32 Percentage { get; set; }
    }

    public class TypeDebt
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeFile
    {
        public string IdType { get; set; }
        public string Description { get; set; }
    }

    public class TypeClassification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IntakeAcronym { get; set; }
        public bool IsActive { get; set; }

        public ICollection<AgreementVM> Agreements { get; set; }
        public ICollection<PreAgreementVM> PreAgreements { get; set; }
    }
}
