using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SeguimientoAlumnoCategoria : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public bool AplicaModalidadOnline { get; set; }
        public bool AplicaModalidadAonline { get; set; }
        public bool AplicaModalidadPresencial { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
        public int? IdSeguimientoAlumnoDetalle { get; set; }
    }
}
