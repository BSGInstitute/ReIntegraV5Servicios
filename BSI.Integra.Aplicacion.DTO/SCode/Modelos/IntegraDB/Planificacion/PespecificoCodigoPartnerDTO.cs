using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class PespecificoCodigoPartnerDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public string? Codigo { get; set; }
        public int? Pdu { get; set; }
        public int IdPGeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
    }
}
