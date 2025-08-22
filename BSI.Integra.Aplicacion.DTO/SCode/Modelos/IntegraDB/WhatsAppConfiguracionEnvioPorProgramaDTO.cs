using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppConfiguracionEnvioPorProgramaDTO : BaseIntegraEntity
    {
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPgeneral { get; set; }
        public int IdTipoEnvioPrograma { get; set; }
    }

    public class SmsConfiguracionEnvioPorProgramaDTO : BaseIntegraEntity
    {
        public int IdSmsConfiguracionEnvio { get; set; }
        public int IdPGeneral { get; set; }
        public int IdTipoEnvioPrograma { get; set; }
    }
}
