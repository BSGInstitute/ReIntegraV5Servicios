using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralCertificacion : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(400)]
        public string Nombre { get; set; } = null!;
        public ICollection<ProgramaGeneralCertificacionArgumento> ProgramaGeneralCertificacionArgumentos { get; set; }
        public ICollection<ProgramaGeneralCertificacionModalidad> ProgramaGeneralCertificacionModalidads { get; set; }
    }
}
