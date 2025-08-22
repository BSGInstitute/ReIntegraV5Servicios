using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial
{
    public class AsesorMarcadorDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public bool MarcadorActivo { get; set; }
    }
    public class ReporteAsesorMarcadorDTO
    {
        public int IdPersonal { get; set; }
        public bool? MarcadorActivo { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ReporteFinalMarcadorDTO
    {
        public int Hora { get; set; }
        public int NumVecesDetenido { get; set; }
        public double TiempoTotalDetenido { get; set; }
        public double TiempoPromedioDetencion { get; set; }
        public double PerTiempoDetenido { get; set; }
    }
    public class ReporteFinalPromedioDTO
    {
        public int Orden { get; set; }
        public int Cantidad { get; set; }
        public string AMD { get; set; }
        public string Tipo { get; set; }
        public int PromedioSegundo { get; set; }
        public double PromedioMin { get; set; }

    }
    public class FiltroReporteAsesorMarcadorDTO
    {
        public string Asesores { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ReporteFinalMarcadorTotalDTO
    {
        
        public List<ReporteFinalMarcadorDTO> TiemposMuertos { get; set; }
        public List<ReporteFinalPromedioDTO> TiemposPromedios { get; set; }


    }
}
