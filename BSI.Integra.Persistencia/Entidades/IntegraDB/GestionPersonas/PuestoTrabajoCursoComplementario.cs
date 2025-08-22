using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoCursoComplementario : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdTipoCompetenciaTecnica { get; set; }
        public int IdCompetenciaTecnica { get; set; }
        public int? IdNivelCompetenciaTecnica { get; set; }
        public int? IdMigracion { get; set; }
    }
}
