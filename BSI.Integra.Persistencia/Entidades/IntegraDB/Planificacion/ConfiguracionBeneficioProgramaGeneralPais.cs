using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ConfiguracionBeneficioProgramaGeneralPais : BaseIntegraEntity
    {
        public int IdConfiguracionBeneficioPgneral { get; set; }
        public int IdPais { get; set; }
        public int? IdMigracion { get; set; }
    }
}
