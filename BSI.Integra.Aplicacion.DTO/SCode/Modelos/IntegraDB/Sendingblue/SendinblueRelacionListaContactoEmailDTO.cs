using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendinblueRelacionListaContactoEmailDTO : BaseIntegraEntity
    {
        public string Email { get; set; } = null!;
        public int IdSendinblueLista { get; set; }
    }
}
