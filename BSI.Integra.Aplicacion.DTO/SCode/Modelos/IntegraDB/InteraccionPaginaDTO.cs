using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    internal class InteraccionPaginaDTO
    {
    }
    public class InteraccionAlumnoDTO
    {
        public string Fecha { get; set; }
        public string Hora { get; set; }
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string TipoInteraccion { get; set; }
        public int Duracion { get; set; }
        public string Asesor { get; set; }
    }
}
