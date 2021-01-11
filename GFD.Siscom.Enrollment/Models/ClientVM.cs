using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class ClientVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string SecondLastName { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string INE { get; set; }
        public string EMail { get; set; }
        public string TypeUser { get; set; }
        public bool TaxRegime { get; set; }
        public bool IsMale { get; set; }
        public bool IsActive { get; set; }
        public List<ContactVM> Contacts { get; set; }
    }
}
