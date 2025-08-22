using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteTipoDeCambioFinancieroMensualDTO
    {
        public int Id { get; set; }

        public string MonedaAdolar { get; set; } 

        public string DolarAmoneda { get; set; } 

        public int Mes { get; set; }

        public string? NombreMes { get; set; } = "";


        public int Anio { get; set; }

        public int IdMoneda { get; set; }
    }

    public class ReporteTipoDeCambioFinancieroMensualEnvioDTO
    {
        public int Id { get; set; }
        public string MonedaAdolar { get; set; } 
        public string DolarAmoneda { get; set; } 
        public int Mes { get; set; }
        public string NombreMes { get; set; }
        public int Anio { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }

    }

    public class ReporteTipoDeCambioFinanzcieroMensualGrillaDTO
    {

        public int? Id { get; set; }
        public int Mes { get; set; }

        public int Anio { get; set; }

        public int IdMoneda { get; set; }
    }



}
