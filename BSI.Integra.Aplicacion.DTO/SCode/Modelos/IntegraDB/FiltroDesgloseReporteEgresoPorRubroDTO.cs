using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroDesgloseReporteEgresoPorRubroDTO
    {
        public string IdEmpresa { get; set; }
        public DateTime FechaInicio  { get; set; }
        public DateTime FechaFin  { get; set; }
        public int IdRubro { get; set; }
    }
}
