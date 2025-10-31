using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookFormularioLeadgenLog: BaseIntegraEntity
    {
        public int IdFacebookFormularioLeadgen { get; set; }
        public string JsonApiFacebook { get; set; } = null!;
        public string RespuestaApiFacebook { get; set; } = null!;
        public bool EsError { get; set; }
        public bool Evento { get; set; }
        public bool Pixel { get; set; }
    }
}
