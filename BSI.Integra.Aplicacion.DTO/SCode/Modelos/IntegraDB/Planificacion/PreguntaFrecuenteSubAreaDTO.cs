using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaFrecuenteSubAreaDTO
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int IdSubArea { get; set; }
    }
}
