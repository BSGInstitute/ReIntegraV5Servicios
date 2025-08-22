using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class CursoComplementarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoCursoComplementario { get; set; }
        public string TipoCursoComplementario { get; set; }
        public int? IdNivelCompetenciaTecnica { get; set; }
        public virtual TNivelCompetenciaTecnica? IdNivelCompetenciaTecnicaNavigation { get; set; }
        public virtual TPerfilPuestoTrabajo IdPerfilPuestoTrabajoNavigation { get; set; } = null!;
        public virtual TTipoCompetenciaTecnica IdTipoCompetenciaTecnicaNavigation { get; set; } = null!;
    }

    public class TipoCursoComplementarioDTO
    {
        public int IdTipoCursoComplementario { get; set; }
        public string Nombre { get; set; }
    }
    public class TipoFormacionAcademicaDTO
    {
        public int IdTipoFormacion { get; set; }
        public string Nombre { get; set; }
    }
    public class TipoCompetenciaTecnicaDTO
    {
        public string Nombre
        {
            get; set;
        }
    }
}
