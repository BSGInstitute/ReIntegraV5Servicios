using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ConfiguracionBeneficioProgramaGeneralDatoAdicional : BaseIntegraEntity
    {
        public int IdConfiguracionBeneficioPgeneral { get; set; }
        public int IdBeneficioDatoAdicional { get; set; }
        public int? IdMigracion { get; set; }

    }
}
