using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaIntentoDetalleDTO
    {
        public int? Id { get; set; }
        public int IdPreguntaIntento { get; set; }
        public int? PorcentajeCalificacion { get; set; }
    }
    public class PreguntaIntentoDetalleOrdenDTO
    {
        public int IdPreguntaIntento { get; set; }
        public int Orden { get; set; }
        public int PorcentajeCalificacion { get; set; }

    }
}
