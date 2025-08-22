using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralModeloCertificado : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        [StringLength(400)]
        public string Nombre { get; set; } = null!;
        [StringLength(500)]
        public string Url { get; set; } = null!;
        public ICollection<ProgramaGeneralModeloCertificadoModalidad> ProgramaGeneralModeloCertificadoModalidads { get; set; }
    }
}
