using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendinBlueEventoWebHookDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
    }
    public class SendinBlueEventoWebHook : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
    }
}
