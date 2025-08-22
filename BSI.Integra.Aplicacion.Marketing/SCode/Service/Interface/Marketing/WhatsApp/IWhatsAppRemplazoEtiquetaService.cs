using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IWhatsAppRemplazoEtiquetaService
    {
        object FinalizarPreProcesamientoWhatsApp(PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PreprocesamientoWhatsAppCampaniaGeneral);
    }
}
