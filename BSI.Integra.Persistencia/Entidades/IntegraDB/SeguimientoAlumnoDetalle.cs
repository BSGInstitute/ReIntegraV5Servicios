using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SeguimientoAlumnoDetalle : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdEstadoMatricula { get; set; }
        public int IdSubEstadoMatricula { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public int? IdMigracion { get; set; }
    }
}
