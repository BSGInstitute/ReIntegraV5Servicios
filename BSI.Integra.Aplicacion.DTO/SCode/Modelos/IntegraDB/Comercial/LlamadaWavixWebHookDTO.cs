using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class LlamadaWavixWebHookDTO
    {
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public string EstadoLlamada { get; set; }
        public string OcurrenciaLlamada { get; set; }
        public string TroncalSIP { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }

    }

    public class LlamadaWavixEntranteDTO
    {
        public string Uuid { get; set; }
        public string TroncalSIP { get; set; }
        public string Destino { get; set; }
        public string Origen { get; set; }
        public DateTime FechaLlamada { get; set; }


    }
}