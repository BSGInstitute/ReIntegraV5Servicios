using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoParticipacionExpositor : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int? Orden { get; set; }
        public int? Grupo { get; set; }
        public int? IdExpositorCurso { get; set; }
        public string? ExpositorCurso { get; set; }
        public int? IdExpositorGrupo { get; set; }
        public string? ExpositorGrupo { get; set; }
        public int? IdExpositorV3 { get; set; }
        public string? ExpositorV3 { get; set; }
        public int? IdExpositorGrupoConfirmado { get; set; }
        public int? IdProveedorFurHonorario { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProveedorPlanificacionGrupo { get; set; }
        public int? IdProveedorOperacionesGrupoConfirmado { get; set; }
        public bool? EsSilaboAprobado { get; set; }
    }
}
