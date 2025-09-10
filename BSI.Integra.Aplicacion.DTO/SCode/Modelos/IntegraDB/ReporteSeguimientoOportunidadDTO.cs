using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteSeguimientoOportunidadDTO
    {
        public int? Id { get; set; }
        public string Area { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseoportunidadIC { get; set; }
        public string CodigoPagoIC { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string Asesor { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string NombrePais { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public decimal? ProbabilidadActualValor { get; set; }
        public string probabilidadActual { get; set; }
        public decimal? ProbabilidadNuevoValor { get; set; }
        public string probabilidadNuevo { get; set; }
        public string CodigoPago { get; set; }
        public string Sentinel { get; set; }
        public string NombreGrupo { get; set; }
        public double? PrecioLista { get; set; }
        public double? PrecioListaDolares { get; set; }
        public double? MontoTotal { get; set; }
        public double? MontoTotalDolares { get; set; }
        public string Moneda { get; set; }
        public double? TotalPago { get; set; }
        public double? MontoPagado { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoActividadDetalle { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string Verificado { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public string Paquete { get; set; }
        public double? Matricula { get; set; }
        public double? MatriculaDolares { get; set; }
        public int? IdMatriculaObservacion { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        public string Descuento { get; set; }
        public string NombreCampania { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public decimal? Probabilidad1 { get; set; }
        public decimal? Probabilidad2 { get; set; }
        public string Clasificacion1 { get; set; }
        public string Clasificacion2 { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? IdBusqueda { get; set; }
        public string Interaccion { get; set; }
        public string UrlOrigen { get; set; }

        public string GrabacionIntegra { get; set; }
        public string GrabacionCentral { get; set; }
        public string ConvenioFirmado { get; set; }
        public string PersonaEncargada { get; set; }
        public string? Webphone { get; set; }

        public bool? AccesoTemporal { get; set; }
        public string ProgramaAccesoTemporal { get; set; }
        public string FechaInicioAccesoTemporal { get; set; }
        public string FechaFinAccesoTemporal { get; set; }

        public string CoordinadoraAcademica { get; set; }

        //visualizacion
        public string AsesorSolicitante { get; set; }
        public Nullable<int> IdPersonalSolicitante { get; set; }
        public Nullable<int> IdSolicitudVisualizacion { get; set; }

        //Nuevos campos  JC 30062023

        public int MinutosHabladosFaseActual { get; set; }
        public int MinutosHabladosTotal { get; set; }
        public int ActReproManTotal { get; set; }
        public int ActReproManConsecutivasTotal { get; set; }

    }
    public class ReporteSeguimientoOportunidadesOperacionesDTO
    {
        public int? Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseoportunidadIC { get; set; }
        public string CodigoPagoIC { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string Asesor { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string NombrePais { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string probabilidadActual { get; set; }
        public string CodigoPago { get; set; }
        public string Sentinel { get; set; }
        public string NombreGrupo { get; set; }
        public double? MontoTotal { get; set; }
        public string Moneda { get; set; }
        public double? TotalPago { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoActividadDetalle { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string Verificado { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public string CodigoMatricula { get; set; }
        public int? DiasAtrasoCuotaPago { get; set; }
        public int? CuotasAtrasoCuotaPago { get; set; }
        public decimal? MontoAtrasoCuotaPago { get; set; }
        public string MonedaCuotaPago { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public string AgendaTab { get; set; }
        public string DiasSeguimientoActividadesEjecutadas { get; set; }
        public string UltimoContacto { get; set; }
        public string DiasSeguimientoActividadesEjecutadas7dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas14dias { get; set; }
        public string DiasSeguimientoActividadesEjecutadas30dias { get; set; }
        public int? NroCuotasAtrasadas { get; set; }
        public decimal? MontoCuotasAtrasadas { get; set; }
        public string EstadoAcademico { get; set; }
        public string Paquete { get; set; }
        public string GrabacionCentral { get; set; }
        public string ConvenioFirmado { get; set; }
        public string PersonaEncargada { get; set; }
        public int IdCriterioCalificacion { get; set; }
        public int IdMatriculaObservacion { get; set; }
        public int? IdPEspecifico { get; set; }
        public int? ValorAvanceReal { get; set; }
        public string AvanceReal { get; set; }
        public string AvanceRealSesion { get; set; }
        public string AvanceRealAutoevaluacion { get; set; }
        public string AvanceProgramado { get; set; }
        public string AvanceProgramadoSesion { get; set; }
        public string AvanceProgramadoAutoevaluacion { get; set; }
        public int? ValorAvanceProgramado { get; set; }
        public int? ReproduccionVideoReal { get; set; }
        public int? ReproduccionVideoProgramado { get; set; }
        public int? CumplimientoAvance { get; set; }
        public int? DiasDesdeUltimoAvance { get; set; }
        public bool? AccesoTemporal { get; set; }
        public string ProgramaAccesoTemporal { get; set; }
        public string FechaInicioAccesoTemporal { get; set; }
        public string FechaFinAccesoTemporal { get; set; }
        public string AulaVirtual { get; set; }
        public string FechaFinalizacion { get; set; }
    }

    public class ReporteInscritosCarreraOperacionesDTO
    {
        public int? Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdAlumno { get; set; }
        public string NroDocumento { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string Asesor { get; set; }
        public string Ciudad { get; set; }
        public string Celular { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdPgeneral { get; set; }
        public string NombrePGeneral { get; set; }
        public int? IdCiclo { get; set; }
        public string Ciclo { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public DateTime? FechaInicioCiclo { get; set; }
        public DateTime? FechaInicioCurso { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string TipoMatricula { get; set; }
        public string Modalidad { get; set; }
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? EdadActual { get; set; }
        public string Pais { get; set; }
        public string Industria { get; set; }
        public string CentroLaboral { get; set; }
        public string Cargo { get; set; }
        public string SituacionLaboral { get; set; }
        public string LugarTrabajo { get; set; }
        public string RangoRemuneracion { get; set; }

    }
    public class ReporteSeguimientoOportunidadesModeloDTO
    {
        public int? Id { get; set; }
        public string Area { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string Asesor { get; set; }
        public string CodigoFaseOportunidad { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public string NombrePais { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string NombreGrupo { get; set; }
        public DateTime? FechaReal { get; set; }
        public string EstadoOcurrencia { get; set; }
        public DateTime? FechaCierre { get; set; }
        public decimal? ProbabilidadActualValor { get; set; }
        public string ProbabilidadActual { get; set; }
        public decimal? ProbabilidadNuevoValor { get; set; }
        public string ProbabilidadNuevo { get; set; }

    }
    public class ReporteSeguimientoOportunidadCombosDTO
    {
        public List<ComboDTO> CentroCostos { get; set; }
        public List<FaseOportunidadComboDTO> FaseOportunidades { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public List<EstadoMatriculaComboDTO> Estados { get; set; }
    }
    public class ReporteSeguimientoOportunidadesFiltrosDTO
    {
        public List<int> CentroCostos { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> FasesOportunidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int OpcionFase { get; set; }
        public List<int> FaseOportunidadOrigen { get; set; }
        public List<int> FaseOportunidadDestino { get; set; }
        public string? DocumentoIdentidad { get; set; }
        public string? CodigoMatricula { get; set; }
        public List<int>? EstadosMatricula { get; set; }
        public int? TipoFecha { get; set; }
        public int? ControlFiltroFecha { get; set; }
    }
    public class SeguimientoFiltroFinalDTO
    {
        public string CentroCostos { get; set; }
        public string Asesores { get; set; }
        public string FasesOportunidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int OpcionFase { get; set; }
        public string FasesOportunidadOrigen { get; set; }
        public string FasesOportunidadDestino { get; set; }
        public string EstadosMatricula { get; set; }
        public string DocumentoIdentidad { get; set; }
        public string CodigoMatricula { get; set; }
        public int? ControlFiltroFecha { get; set; }
    }
    public class ReporteSolicitudesVisualizacionFiltroDTO
    {
        public List<int> CentroCostos { get; set; }
        public List<int> Asesores { get; set; }
        public List<int> FasesOportunidad { get; set; }
    }
    public class AprobacionSolicitudesVisualizacionFiltroDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int IdSolicitudVisualizacion { get; set; }
        public string? Usuario { get; set; }
    }
    public class OportunidadesNoEjecutadasDTO
    {
        public String Id { get; set; }
        public DateTime FechaProgramada { get; set; }
        public int? IdCodigoPais { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public int? IdFaseOportunidad { get; set; }
    }
    public class OportunidadesACerrarBICDTO
    {
        public int Id { get; set; }

    }
    public class ReporteSeguimientoOportunidadLogGridDTO
    {
        public int IdActividadDetalle { get; set; }
        public string FaseInicio { get; set; }
        public string FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string EstadoFase { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaPago { get; set; }
        public string Estado { get; set; }
        public string TiempoDuracion { get; set; }
        public string TiempoDuracion3CX { get; set; }
        public List<LlamadaIntegraDTO> LlamadaIntegra { get; set; }
        public List<LlamadaIntegraDTO> LlamadaTresCX { get; set; }
        public string NombreActividad { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string ComentarioPago { get; set; }
        public string ComentarioAcademico { get; set; } 
    }
    public class OportunidadLogReporteATCDTO
    {
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int IdActividadDetalle { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseOportunidadIC { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadIC { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public int IdOportunidadLog { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public int IdContacto { get; set; }
        public bool OtroMedio { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public int? IdRegistroLlamada { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public string? EstadoLlamada { get; set; }
        public string? SubEstadoLlamada { get; set; }
        public string? NombreGrabacion { get; set; }
        public string? UrlGrabacion { get; set; }
        public string? WebphoneGrabacion { get; set; }
        public string? TelefonoDestinoReal { get; set; }
        public string? TelefonoDestino { get; set; }
        public string? AnexoCentral { get; set; }
        public string? OrigenLlamada { get; set; }
        
    }
    public class OportunidadLogReporteOperacionesDTO
    {
        public List<OportunidadLogReporteATCDTO> Items { get; set; }
        public int? TotalActividades { get; set; }
        
    }
    public class ReporteSeguimientoOportunidadLogDTO
    {
        public string FaseInicio { get; set; }
        public string FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseOportunidadIC { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadIC { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string TiempoDuracion { get; set; }
        public string TiempoDuracionMinutos { get; set; }
        public string TiempoDuracion3CX { get; set; }
        public int? IdCentralLLamada { get; set; }
        public int? IdTresCX { get; set; }
        public int IdOportunidadLog { get; set; }
        public DateTime? FechaIncioLlamadaIntegra { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public List<LlamadaIntegraDTO> LlamadaIntegra { get; set; }
        public List<LlamadaIntegraDTO> LlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string EstadoLlamadaIntegra { get; set; }
        public string SubEstadoLlamadaIntegra { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string NombreActividad { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreGrabacionIntegra { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string EstadoLlamadaSegunFlow { get; set; }
        public string Webphone { get; set; }
        public string Personal { get; set; }
    }
    public class OportunidadLogReporteSeguimientoDetalleATCDTO
    {
        public int IdOportunidadLog { get; set; }
        public int IdActividadDetalle { get; set; }
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseOportunidadIC { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadPF { get; set; }
        public DateTime? FechaPagoFaseOportunidadIC { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }
        public List<LlamadaIntegra3cxDTO> Llamadas { get; set; } = new List<LlamadaIntegra3cxDTO>();
    }
    public class OportunidadLogReporteSeguimientoDetalleOperacionesDTO
    {
        public List<OportunidadLogReporteSeguimientoDetalleATCDTO> Items { get; set; }
        public int? TotalActividades { get; set; }
    }
        public class ReporteSeguimientoNWActividadAlternoATCDTO
    {
        public int IdActividadDetalle { get; set; }
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public string? ComentarioAcademico { get; set; }
        public string? ComentarioPago { get; set; }
        public int? TotalEjecutadas { get; set; }
        public int? TotalNoEjecutadas { get; set; }
        public int? TotalAsignacionManual { get; set; }
        public string? EstadoFase { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }
        public List<LlamadaIntegra3cxDTO> LlamadasIntegra3cx { get; set; } = new List<LlamadaIntegra3cxDTO>();
    }

    public class ReporteSeguimientoNWActividadAlternoOperacionesDTO
    { 
        public List<ReporteSeguimientoNWActividadAlternoATCDTO> Items { get; set; }
        public int? TotalActividades { get; set; }
    }
        public class EditarActividadLlamadaDTO
    {
        public IFormFile File { get; set; }
        public int IdLlamada { get; set; }
        public string NombreArchivo { get; set; }
        public int DuracionContesto { get; set; }
        public int NroBytes { get; set; }
    }
    public class NuevaLlamadaActividadDTO
    {
        public int IdActividadDetalle { get; set; }
        public int IdPersonalAsignado { get; set; }
        public bool? GrabacionContrato { get; set; }
        public string Anexo3CX { get; set; }
        public DateTime FechaInicio { get; set; }
        public string TelefonoDestino { get; set; }
        public IFormFile File { get; set; }
        public int IdLlamada { get; set; }
        public string NombreArchivo { get; set; }
        public int DuracionContesto { get; set; }
        public int NroBytes { get; set; }
    }

    public class DatosLlamadaDTO
    {
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public string Anexo3CX { get; set; }
        public string Central { get; set; }
    }
    public class ReporteSeguimientoInscritoComboDTO
    {
        public IEnumerable<ComboDTO> CentroCostos { get; set; }
        public List<FaseOportunidadComboDTO> FaseOportunidades { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public List<EstadosMatriculaDTO> Estados { get; set; }
    }
    public class ReporteSeguimientoOportunidadComboDTO
    {
        public List<ComboDTO> CentroCostos { get; set; }
        public List<FaseOportunidadComboDTO> FaseOportunidades { get; set; }
        public List<PersonalAsignadoDTO> Asesores { get; set; }
        public List<ComboDTO> CriteriosCalificacion { get; set; }
        public List<ComboDTO> ObservacionMatricula { get; set; }
    }
}
