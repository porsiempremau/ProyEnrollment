using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class SuburbVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RegionsId { get; set; }
        public int ClasificationsId { get; set; }
        public int SuburbId { get; set; }
        public TownVM Towns { get; set; }
        public int TownsId { get; set; }
    }
}
