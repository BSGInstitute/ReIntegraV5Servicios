using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SesionPlanificacionDTO
    {
        public DateTime? FechaSesion { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public string Tema { get; set; }
    }
}