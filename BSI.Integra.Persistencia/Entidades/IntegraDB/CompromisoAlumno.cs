using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CompromisoAlumno : BaseIntegraEntity
    {
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public DateTime FechaGeneracionCompromiso { get; set; }
        public decimal Monto { get; set; }
        public int? IdMoneda { get; set; }
        public int Version { get; set; }
    }
}
