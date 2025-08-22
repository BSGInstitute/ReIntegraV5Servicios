using Microsoft.AspNetCore.Http.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueCampaniasEnvioApiDTO;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class UpdateCampania
    {

        public class UpdateCampaniaDTO
        {
            public Sender sender { get; set; }
            public Recipients recipients { get; set; }
            public bool inlineImageActivation { get; set; } = false;
            public bool recurring { get; set; } = false;
            public bool abTesting { get; set; } =false;
            public bool ipWarmupEnable { get; set; } = false;
            public string name { get; set; }
            public string subject { get; set; }
            public string scheduledAt { get; set; }
            public string subjectA { get; set; }
            public string subjectB { get; set; }
        }
    }
}
