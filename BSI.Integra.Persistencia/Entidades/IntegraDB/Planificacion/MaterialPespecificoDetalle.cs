using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialPespecificoDetalle : BaseIntegraEntity
    {
        public int IdMaterialPespecifico { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFur { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioSubida { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? IdEstadoRegistroMaterial { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdMaterialTipo { get; set; }
        public List<MaterialAccion> ListaMaterialAccion { get; set; }
    }
}
