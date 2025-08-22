using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralMotivacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(400)]
        public string Nombre { get; set; } = null!;
        public ICollection<ProgramaGeneralMotivacionArgumento> ProgramaGeneralMotivacionArgumentos { get; set; }
        public ICollection<ProgramaGeneralMotivacionModalidad> ProgramaGeneralMotivacionModalidads { get; set; }
    }
}
