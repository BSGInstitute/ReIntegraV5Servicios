using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionEnvioMailingDetalle : BaseIntegraEntity
    {
        public int IdConfiguracionEnvioMailing { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public string Asunto { get; set; }
        public string CuerpoHtml { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public int? IdMigracion { get; set; }
        public int IdMandrilEnvioCorreo { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdOportunidad { get; set; }

    }
}
