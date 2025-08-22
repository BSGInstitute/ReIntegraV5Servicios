using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumnoEstadoMatricula : BaseIntegraEntity
    {
        public int IdEstadoMatricula { get; set; }
        public int IdTipoDocumentoAlumno { get; set; }
    }
}
