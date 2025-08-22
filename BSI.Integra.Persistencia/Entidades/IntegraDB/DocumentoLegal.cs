using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoLegal : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(300)]
        public string? Descripcion { get; set; }
        public int IdPais { get; set; }
        [StringLength(1000)]
        public string Url { get; set; } = null!;
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string? Roles { get; set; }

    }
}
