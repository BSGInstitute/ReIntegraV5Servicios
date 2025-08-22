using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoDocumentoAlumnoPgeneral : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int IdTipoDocumentoAlumno { get; set; }
    }
}
