using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteOportunidadDetalleDTO
    {
        public List<OportunidadVentaCruzadaDTO> listaOportunidadVentaCruzada { get; set; }
        public AlumnoInformacionDTO datosAlumno { get; set; }
        public ProgramaGeneralPreBenCompuestoDTO ProgramaGeneralPreBen { get; set; }
        public List<OportunidadProblemaClienteDTO> ListaProblemaCliente { get; set; }
        public ReporteSeguimientoOportunidadComplementosDTO OportunidadComplementos { get; set; }
        public SueldosDescripcionDTO probabilidadsueldo { get; set; }
        public int idFaseOportunidad { get; set; }
        public int idActividadDetalle { get; set; }
        public string nombresPersonal { get; set; }
        public string apellidosPersonal { get; set; }

    }
    public class OportunidadProblemaClienteDTO
    {
        public int IdProblema { get; set; }
        public string NombreProblema { get; set; }
        public int IdCausa { get; set; }
        public string NombreCausa { get; set; }
        public int IdSolucion { get; set; }
        public string NombreSolucion { get; set; }
        public string DescripcionSolucion { get; set; }
        public string Seleccionado { get; set; }
        public string Solucionado { get; set; }
        public string OtroProblema { get; set; }
    }
    public class ReporteSeguimientoOportunidadComplementosDTO
    {
        public string ProbabilidadActual { get; set; }
        public string CodigoFase { get; set; }
        public string CategoriaOrigen { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonalAsignado{ get; set; }
        public string CentroCosto { get; set; }
        public string Celular { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
    }
    public class SueldosDescripcionDTO
    {
        public int? valor { get; set; }
        public string descripcion { get; set; }
    }
    public class ReporteOportunidadOperacionesDetalleDTO
    {
        public AlumnoInformacionDTO datosAlumno { get; set; }
        public ReporteSeguimientoOportunidadComplementosDTO OportunidadComplementos { get; set; }
    }
}
