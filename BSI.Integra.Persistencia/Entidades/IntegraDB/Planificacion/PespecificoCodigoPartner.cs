using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoCodigoPartner : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public string? Codigo { get; set; }
        public int? Pdu { get; set; }
        public DateTime? FechaInicio { get; set; }
    }
}
