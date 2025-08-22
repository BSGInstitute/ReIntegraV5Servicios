using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadCampaniaGeneralWhatsappParaEjecutarDTO
    {
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdCampaniaGeneralDetalleResponsable { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPlantilla { get; set; }
        public int Dia1 { get; set; }
        public int Dia2 { get; set; }
        public int Dia3 { get; set; }
        public int Dia4 { get; set; }
        public int Dia5 { get; set; }
        public int Dia { get; set; }
        public DateTime FechaInicioEnvioWhatsapp { get; set; }
        public DateTime FechaFinEnvioWhatsapp { get; set; }
        public TimeSpan HoraEnvio { get; set; }

    }
}
