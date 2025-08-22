using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class FiltroAnalisisProcesoSeleccionDTO
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdProcesoSeleccion { get; set; }
    }
    public class ReporteAnalisisProcesoSeleccionDTO
    {
        public int IdEtapa { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string NombreEtapa { get; set; }
        public int? IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public int OrdenEtapa { get; set; }
        public int NumeroPostulante { get; set; }
        public int Contactados { get; set; }
        public int Aprobados { get; set; }
        public int Desaprobados { get; set; }
        public int EnProceso { get; set; }
        public int Abandonados { get; set; }
        public int SinRendir { get; set; }
    }
    public class ReporteAnalisisProcesoSeleccionPorcentajeDTO
    {
        public int IdEtapa { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string NombreEtapa { get; set; }
        public int? IdProveedor { get; set; }
        public string Proveedor { get; set; }
        public int OrdenEtapa { get; set; }
        public string NumeroPostulante { get; set; }
        public string Contactados { get; set; }
        public string Aprobados { get; set; }
        public string Desaprobados { get; set; }
        public string EnProceso { get; set; }
        public string Abandonados { get; set; }
        public string SinRendir { get; set; }
    }

    public class ReportePrincipalAnalisisProcesoSeleccionDTO
    {
        public List<ReporteAnalisisProcesoSeleccionDTO> listaEtapas { get; set; }
        public List<ReporteAnalisisProcesoSeleccionPorcentajeDTO> listaEtapasPorcentaje { get; set; }
    }


    public class ReporteFiltroAnalisisDTO
    {
        public IEnumerable<ComboDTO> listaPuestoTrabajo { get; set; }
        public IEnumerable<ProcesoSeleccionComboReporteDTO> listaProcesoSeleccion { get; set; }
    }

}
