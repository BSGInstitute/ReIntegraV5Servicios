using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralArgumentoDetalleMotivacion
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumentoDetalle { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public string? NombreMotivacion { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
