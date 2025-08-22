using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadPrerequisitoEspecifico : BaseIntegraEntity
    {
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdProgramaGeneralPrerequisito { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;

    }
}
