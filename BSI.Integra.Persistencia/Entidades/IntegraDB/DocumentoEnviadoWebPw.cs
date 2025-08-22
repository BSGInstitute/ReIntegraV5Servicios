using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoEnviadoWebPw : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
    }
}
