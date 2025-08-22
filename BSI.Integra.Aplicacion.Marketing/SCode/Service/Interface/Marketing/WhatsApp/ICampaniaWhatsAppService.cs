using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface ICampaniaWhatsAppService
    {
        RespuestaGenerica ObtenerPrioridadesDeFiltroWpp(int idCampaniaGeneral, int idCampaniaGeneralDetalle);
    }
}
