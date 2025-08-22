using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaIntentoDTO
    {
        public int? Id { get; set; }
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string? MensajeFeedback { get; set; }
        public List<PreguntaIntentoDetalleDTO> PreguntaIntentoDetalles { get; set; }
    }
}
