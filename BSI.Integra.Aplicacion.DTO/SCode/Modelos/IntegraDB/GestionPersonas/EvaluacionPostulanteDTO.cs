
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona
{
    public class EvaluacionPostulanteDTO
    {
        public int? IdEvaluacion { get; set; }
        public List<NotaPostulanteDTO> ListaComponentesEvaluacion { get; set; }
    }
    public class EvaluacionPostulanteComboDTO
    {
        public IEnumerable<ComboDTO> EstadoEtapas { get; set; }
        public IEnumerable<ComboDTO> ProcesosDeSeleccion { get; set; }
        public IEnumerable<ProcesoSeleccionEtapaDTO> ProcesoSeleccionEtapas { get; set; }
        public IEnumerable<ComboDTO> GruposComparacion { get; set; }
        public IEnumerable<StringDTO> VersionesCentil { get; set; }
    }
    public class EvaluacionPostulanteFiltroReporteDTO
    {
        public List<int> IdsPostulantes { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? IdGrupoComparacion { get; set; }
        public List<int> IdsProcesoEtapa { get; set; }
        public List<int> IdsEstadoEtapa { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool FiltroPorPostulante { get; set; }
        public List<int>? idsPostulanteGrupoComparacion { get; set; }
        public int? IdProcesoSeleccionGrupoComparacion { get; set; }
    }
    public class ProcesoSelecionExamenesCompletosDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int Edad { get; set; }
        public string Examen { get; set; }
        public int? IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdExamen { get; set; }
        public int VersionCentil { get; set; }
        public string Evaluacion { get; set; }
        public int? IdEvaluacion { get; set; }
        public string Grupo { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdEtapa { get; set; }
        public string Etapa { get; set; }
        public string NotaAprobatoria { get; set; }
        public string Simbolo { get; set; }
        public string Registro { get; set; }
        public int Orden { get; set; }
        public bool EsAprobado { get; set; }
        public bool CalificaPorCentil { get; set; }
        public int? IdSexo { get; set; }
        public decimal? OrdenReal { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public bool? EstadoAcceso { get; set; }
        public bool? ConfiguracionComponenteCurso { get; set; }
        public int? IdExamenAccesoTemporal { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
        public List<ExamenCentilVersionDTO> ExamenCentilVersion { get; set; }
    }
    public class ExamenCentilVersionDTO
    {
        public int IdCentil { get; set; }
        public string Registro { get; set; }
        public bool EsAprobado { get; set; }
        public string NotaAprobatoria { get; set; }
        public string Simbolo { get; set; }
        public int Version { get; set; }
        public bool EsVigente { get; set; }
        public bool EsVersionExamen { get; set; }
    }
    public class DatosExamenPostulanteDTO
    {
        public int IdProceso { get; set; }
        public string NombreProceso { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int Edad { get; set; }
        public int IdSexo { get; set; }
        public int IdExamen { get; set; }

        public string NombreExamen { get; set; }
        public string Titulo { get; set; }
        public int? IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal? Puntaje { get; set; }
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public decimal? FactorComponente { get; set; }
        public decimal? FactorGrupo { get; set; }
        public decimal? FactorEvaluacion { get; set; }
        public int? IdFormulaComponente { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public int? IdFormulaEvaluacion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? IdPespecificoCurso { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
        public int VersionCentil { get; set; }
    }
    public class DatosNotaPorPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public string NombreProceso { get; set; }
        public List<NotaPostulanteDTO> NotasPostulante { get; set; }
    }
    public class NotaPostulanteDTO
    {
        public int IdProceso { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdSexo { get; set; }
        public int IdExamen { get; set; }
        public int VersionCentil { get; set; }
        public string NombreExamen { get; set; }
        public int? IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public decimal? Puntaje { get; set; }
        public int? IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int? IdEtapa { get; set; }
        public string NombreEtapa { get; set; }
        public decimal? FactorComponente { get; set; }
        public decimal? FactorGrupo { get; set; }
        public decimal? FactorEvaluacion { get; set; }
        public int? IdFormulaComponente { get; set; }
        public int? IdFormulaGrupo { get; set; }
        public int? IdFormulaEvaluacion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
    }
    public class ProcesoSelecionExamenesCompletosComplementoDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int? IdEvaluacion { get; set; }
        public int? IdGrupo { get; set; }
        public int? IdEtapa { get; set; }
        public int Orden { get; set; }
        public bool EsAprobado { get; set; }
        public bool CalificaPorCentil { get; set; }
        public decimal? OrdenReal { get; set; }
    }
    public class PostulanteClasificacionNeoDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public bool RespuestaAlAzar { get; set; }
        public bool AquiescenciaAq { get; set; }
        public bool NegacionesNe { get; set; }
    }
    public class ReportePruebaDetalleDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int? IdEtapa { get; set; }
        public string Etapa { get; set; }
        public int EstadoEtapa { get; set; }
        public int IdEstadoEtapaProceso { get; set; }
        public bool EtapaContactado { get; set; }
        public int? NroOrden { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }
    public class ReportePruebaDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public List<ReportePruebaDetalleDTO> Etapas { get; set; }
    }
    public class EtapaCalificadaPostulanteProcesoSeleccionDTO
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdProcesoSeleccionEtapa { get; set; }
        public string ProcesoSeleccionEtapa { get; set; }
        public int NroOrden { get; set; }
        public int IdEtapaProcesoSeleccionCalificado { get; set; }
        public int IdEstadoEtapaProcesoSeleccion { get; set; }
        public string EstadoEtapaProcesoSeleccion { get; set; }
        public bool EsEtapaAprobada { get; set; }
        public bool EsContactado { get; set; }
        public bool? EsCalificadoPorPostulante { get; set; }
    }
    public class DatoEvaluacionAgrupadoDTO
    {
        public decimal? OrdenReal { get; set; }
        public List<ProcesoSelecionExamenesCompletosDTO> Proceso { get; set; }

    }
    public class PostulanteEvaluacionAgrupadoDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int Edad { get; set; }
    }
    public class ResultadoReporteEvaluacionPostulante
    {
        public List<DatoEvaluacionAgrupadoDTO> DatosEvaluacionAgrupado { get; set; }
        public List<PostulanteEvaluacionAgrupadoDTO> Postulantes { get; set; }
        public List<ReportePruebaDTO> EtapaAprobada { get; set; }
        public int CantidadEtapaAprobada { get; set; }
        public List<PostulanteClasificacionNeoDTO> ClasificacionNEO { get; set; }
        public List<ValorIntDTO> MatriculaPostulantes { get; set; }
    }
    public class ObtenerCalificacionCentilDTO
    {
        public int Id { get; set; }
        public bool CalificaPorCentil { get; set; }
        public bool EsCalificable { get; set; }
        public int IdProcesoSeleccionRango { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
        public int? VersionCentil { get; set; }
        public bool? EsVigente { get; set; }
        public int? IdExamenTest { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public decimal? PuntajeMinimo { get; set; }
        public int? IdCentil { get; set; }
        public decimal? Centil { get; set; }
        public int? IdSexoCentil { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
    }
    public class TipoEvaluacionDTO
    {
        public int TipoEvaluacion { get; set; }
        public int? IdEvaluacion { get; set; }
    }
    public class FiltroTipoExamenDTO
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int? IdEtapa { get; set; }
    }
    public class TestInformacionDTO
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public int IdTest { get; set; }
        public bool MostrarEvaluacionAgrupado { get; set; }
        public bool MostrarEvaluacionPorGrupo { get; set; }
        public bool MostrarEvaluacionPorComponente { get; set; }
    }
    public class CalificacionManualDTO
    {
        public int IdEstadoEA { get; set; }
        public int IdProcesoSeleccionEA { get; set; }
        public int IdProcesoSeleccionEtapaEA { get; set; }
        public int IdPostulanteEA { get; set; }
    }
    public class EnviarAccesoPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public int IdExamen { get; set; }
        public int IdPlantilla { get; set; }
    }
}
