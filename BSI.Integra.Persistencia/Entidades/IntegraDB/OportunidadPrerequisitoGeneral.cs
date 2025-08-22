using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadPrerequisitoGeneral : BaseIntegraEntity
    {
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdProgramaGeneralPrerequisito { get; set; }
        public int Respuesta { get; set; }
        [StringLength(10)]
        public string Completado { get; set; } = null!;

    }
}
