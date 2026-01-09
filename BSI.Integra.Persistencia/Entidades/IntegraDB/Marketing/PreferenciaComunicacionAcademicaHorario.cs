using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    public class PreferenciaComunicacionAcademicaHorario : BaseIntegraEntity
    {
        public int IdAlumno { get; set; }
        public int IdBloqueHorarioDetalle { get; set; }
    }
}
