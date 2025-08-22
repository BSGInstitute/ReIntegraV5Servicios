using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendinblueReporteMarketingDTO
    {
        public int? IdAlumno { get; set; }
        public string Correo { get; set; }
        public int PrimeraApertura { get; set; }
        public DateTime? FechaApertura { get; set; }
        public int CantidadDeClick { get; set; }
    }
    public class SendinblueReporteParametrosDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
