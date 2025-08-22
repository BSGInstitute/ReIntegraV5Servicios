using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DiaFallidoEvaluadoDTO
    {
        public int Id { get; set; }
        public int IdCampaniaGeneral { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public int IdPersonal { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPlantilla { get; set; }
        public int Dia { get; set; }
        public DateTime FechaEvaluada { get; set; }
        public List<int> ListaDiaConfigurado { get; set; }
        public int Cantidad { get; set; }
    }
}
