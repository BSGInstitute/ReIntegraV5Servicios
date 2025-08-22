using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class MensajesWhatsAppRespondidosDTO
    {
        public List<ResumenMensajesWhatsAppRespondidosDTO> ListaMensajesWhatsAppRespondidos { get; set; }
        public int Total { get; set; }
    }
    public class PalabrasOfensivasDTO
    {
        public string PalabraFiltrada { get; set; }
    }


}
