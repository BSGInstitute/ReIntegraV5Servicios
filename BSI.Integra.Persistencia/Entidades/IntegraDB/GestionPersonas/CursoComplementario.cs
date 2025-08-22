using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class CursoComplementario : BaseIntegraEntity
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public int IdTipoCompetenciaTecnica { get; set; }
        public int IdCompetenciaTecnica { get; set; }
        public int? IdNivelCompetenciaTecnica { get; set; }
        public virtual TNivelCompetenciaTecnica? IdNivelCompetenciaTecnicaNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; } 
        public virtual TTipoCompetenciaTecnica IdTipoCompetenciaTecnicaNavigation { get; set; }
    }
}
