using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class SystemsParametersVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Activo { get; set; }
        public Int16 TipoColumna { get; set; }
        public decimal ColumnaNumero { get; set; }
        public string ColumnaTexto { get; set; }
        public DateTime ColumnaFecha { get; set; }
    }
}
