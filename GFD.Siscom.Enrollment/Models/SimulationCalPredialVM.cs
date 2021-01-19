using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Models
{
    public class SimulationCalPredialVM
    {
        public int id_agreement { get; set; }
        public DateTime fecha_alta { get; set; }
        public string estado { get; set; }
        public string cuenta { get; set; }
        public decimal valor_Catastral { get; set; }
        public decimal terreno { get; set; }
        public decimal construccion { get; set; }
        public int utima_Actualizacion { get; set; }
        public int existeLog { get; set; }
        public string clasificador_Cuota_Limpia { get; set; }
        public string colonia { get; set; }
        public string tipoCuenta { get; set; }
        public string predial_calculado { get; set; }
        public string formulA_APLICADA { get; set; }
        public string limpia_calculado { get; set; }
    }
}
