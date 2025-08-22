using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumnoModalidadCurso : BaseIntegraEntity 
    {
        public int IdModalidad { get; set; }
        public int IdTipoDocumentoAlumno { get; set; }
    }
}
