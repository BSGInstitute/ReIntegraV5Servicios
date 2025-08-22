using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralPrerequisito : BaseIntegraEntity
    {
        public int? IdPgeneral { get; set; }
        [StringLength(500)]
        public string Nombre { get; set; } = null!;
        public int Tipo { get; set; }
        public int? Orden { get; set; }
        public List<ProgramaGeneralPrerequisitoModalidad> ProgramaGeneralPrerequisitoModalidads { get; set; }
    }
}
