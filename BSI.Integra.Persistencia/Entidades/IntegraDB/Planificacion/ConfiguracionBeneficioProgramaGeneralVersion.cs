using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ConfiguracionBeneficioProgramaGeneralVersion : BaseIntegraEntity
    {
        public int IdConfiguracionBeneficioPgneral { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdVersionPrograma { get; set; }

    }
}
