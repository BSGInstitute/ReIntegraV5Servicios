using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralCertificacionModalidad : BaseIntegraEntity
    {
        public int IdProgramaGeneralCertificacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }

    }
}
