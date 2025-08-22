using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppMensajeEnviadoApiComercialDTO
    {
        public class WhatsAppMensajeTextoComDTO
        {
            public string WaTo { get; set; }
            public string WaBody { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
        }
        public class WhatsAppMensajePlantillaComDTO
        {
            public string WaTo { get; set; }
            public string WaCaption { get; set; }
            public string WaBody { get; set; }
            public int WaTypeMensaje { get; set; }
            public int IdPlantilla { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
            public List<DatosPlantillaWhatsAppDTO> DatosPlantillaWhatsApp { get; set; }
        }
        public class WhatsAppMensajeArchivoComDTO
        {
            public string WaTo { get; set; }
            public string WaType { get; set; }
            public string WaLink { get; set; }
            public string WaFileName { get; set; }
            public int IdPais { get; set; }
            public int IdAlumno { get; set; }
            public int? IdPersonal { get; set; }
        }
    }
}
