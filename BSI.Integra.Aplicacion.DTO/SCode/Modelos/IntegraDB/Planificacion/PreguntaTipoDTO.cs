using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaTipoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdTipoRespuesta { get; set; }
    }
    public class PreguntaTipoRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoRespuesta { get; set; }
        public int IdTipoRespuesta { get; set; } 
    }
}
