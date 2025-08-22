using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoCursoComplementarioDTO
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdTipoCompetenciaTecnica { get; set; }
        public int IdCompetenciaTecnica { get; set; }
        public int IdNivelCompetenciaTecnica { get; set; }
        public string TipoCompetenciaTecnica { get; set; }
        public string CompetenciaTecnica { get; set; }
        public string NivelCompetenciaTecnica { get; set; }
        public int Version { get; set; }
    }
}
