using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoOportunidad : BaseIntegraEntity
    {
        public int? IdAlumno { get; set; }
        public int? IdOportunidad { get; set; }
        [StringLength(500)]
        public string NombreArchivo { get; set; } = null!;
        [StringLength(500)]
        public string Ruta { get; set; } = null!;
        public string? Comentario { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdDocumentoOportunidadTipo { get; set; }
    }
}
