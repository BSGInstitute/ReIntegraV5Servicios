using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MensajeProcesarDetalleDTO
    {
        public string NombreCampania { get; set; }
        public string NombreLista { get; set; }
        public int NroIntentos { get; set; }
    }
    public class MensajeProcesarDTO
    {
        public string Nombre { get; set; }
        public List<MensajeProcesarDetalleDTO> ListaDetalle { get; set; }
    }

    public class MensajeWhatsAppFinalizacionProcesadoDTO
    {
        public string Nombre { get; set; }
    }

    public class MensajeProcesarMailingGeneralDTO
    {
        public string Nombre { get; set; }
        public List<MensajeProcesarDetalleDTO> ListaDetalle { get; set; }
        public int IntentosCorrectos { get; set; }
        public int IntentosFallidos { get; set; }
    }
}
