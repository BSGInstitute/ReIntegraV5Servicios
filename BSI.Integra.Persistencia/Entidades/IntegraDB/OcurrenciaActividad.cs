using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OcurrenciaActividad : BaseIntegraEntity
    {
        public int IdOcurrencia { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public bool NodoPadre { get; set; }
    }
}
