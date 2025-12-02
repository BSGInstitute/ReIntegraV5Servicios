using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralArgumentoModalidad
    {
        public int Id { get; set; }
        public int IdProgramaGeneralArgumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }    
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
