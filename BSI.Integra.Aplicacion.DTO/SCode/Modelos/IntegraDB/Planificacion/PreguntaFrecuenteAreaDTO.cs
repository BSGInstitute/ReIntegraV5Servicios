using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaFrecuenteAreaDTO
    {
        public int Id { get; set; }
        public int? IdPreguntaFrecuente { get; set; }
        public int IdArea { get; set; }
    }
}
