using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionEnvioAutomaticoDetalle : BaseIntegraEntity
    {
        public int? IdConfiguracionEnvioAutomatico { get; set; }
        public int? IdTipoEnvioAutomatico { get; set; }
        public int? IdTiempoFrecuencia { get; set; }
        public int? IdPlantilla { get; set; }
        public int? Valor { get; set; }
        public bool? EnvioWhatsApp { get; set; }
        public bool? EnvioCorreo { get; set; }
        public bool? EnvioMensajeTexto { get; set; }
        public TimeSpan? HoraEnvioAutomatico { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
