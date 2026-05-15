using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PreguntaProgramaCapacitacionDificultadDTO
    {
        public int IdPreguntaProgramaCapacitacionDificultad { get; set; }
        public string Codigo { get; set; }
        public string NivelDificultad { get; set; }
        public string Descripcion { get; set; }
        public string ColorHexadecimal { get; set; }
    }

    public class ActualizarDificultadPreguntaDTO
    {
        public int Id { get; set; }
        public int IdPreguntaProgramaCapacitacionDificultad { get; set; }
    }
}
