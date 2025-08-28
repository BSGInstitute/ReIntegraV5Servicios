using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PreguntaIntento : BaseIntegraEntity
    {
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string? MensajeFeedback { get; set; }
        public List<PreguntaIntentoDetalle>? PreguntaIntentoDetalles { get; set; }
        public List<PreguntaProgramaCapacitacion>? PreguntaProgramaCapacitacions { get; set; }
    }
}
