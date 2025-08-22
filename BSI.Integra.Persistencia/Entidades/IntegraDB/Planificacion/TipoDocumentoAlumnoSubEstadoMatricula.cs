using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumnoSubEstadoMatricula : BaseIntegraEntity
    {
        public int IdSubEstadoMatricula { get; set; }
        public int IdTipoDocumentoAlumno { get; set; }
    }
}
