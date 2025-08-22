using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralCertificacionArgumento : BaseIntegraEntity
    {
        public int IdProgramaGeneralCertificacion { get; set; }
        [StringLength(8000)]
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
    }
}
