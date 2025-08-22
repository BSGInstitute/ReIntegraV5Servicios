using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class RevisionNivelPwDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Prioridad { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdRevisionPw { get; set; }
    }
}
