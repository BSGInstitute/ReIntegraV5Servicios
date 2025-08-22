using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadBeneficio : BaseIntegraEntity
    {
        public int? IdOportunidadCompetidor { get; set; }
        public int? IdBeneficio { get; set; }
        public int Respuesta { get; set; }
        [StringLength(10)]
        public string Completado { get; set; } = null!;

    }
}
