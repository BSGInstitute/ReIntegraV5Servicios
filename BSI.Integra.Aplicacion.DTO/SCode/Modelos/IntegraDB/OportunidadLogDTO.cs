namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OportunidadLogDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidadAnt { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string? Comentario { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public bool? FasesActivas { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string? CodigoPagoIc { get; set; }
        public int? IdAsesorAnt { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public DateTime? FechaFinLog { get; set; }
        public DateTime? FechaCambioFase { get; set; }
        public bool? CambioFase { get; set; }
        public DateTime? FechaCambioFaseIs { get; set; }
        public bool? CambioFaseIs { get; set; }
        public DateTime? FechaCambioFaseAnt { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnt { get; set; }
        public int? CambioFaseAsesor { get; set; }
        public int? CicloRn2 { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class OportunidadLogComboDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string FaseAnterior { get; set; } = null!;
        public string FaseActual { get; set; } = null!;
        public DateTime? Fecha_Log { get; set; }
    }
    public class FechaLogDatetimeDTO
    {
        public DateTime? Fecha_Log { get; set; }
    }
    public class OportunidadLogReporteSeguimientoNWDTO
    {
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
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
        public string? TiempoDuracion { get; set; }
        public string? TiempoDuracionMinutos { get; set; }
        public string? TiempoDuracion3CX { get; set; }
        public int? IdCentralLLamada { get; set; }
        public int? IdTresCX { get; set; }
        public DateTime? FechaIncioLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public string? NombreGrabacionIntegra { get; set; }
        public string? NombreGrabacionTresCX { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string? EstadoLlamadaIntegra { get; set; }
        public string? SubEstadoLlamadaIntegra { get; set; }
        public string? EstadoLlamadaTresCX { get; set; }
        public string? SubEstadoLlamadaTresCX { get; set; }
        public int IdOportunidadLog { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public string? Webphone { get; set; }
    }
    public class OportunidadLogReporteSeguimientoAlternoDTO
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
        public int? IdLW { get; set; }
        public int? IdLWCC { get; set; }
        public int? IdTresCxCC { get; set; }
        public int? IdRingover { get; set; }
        public int? DuracionTimbradoLW { get; set; }
        public int? DuracionContestoLW { get; set; }
        public int? DuracionTimbradoCentralLWCC { get; set; }
        public int? DuracionContestoCentralLWCC { get; set; }
        public int? DuracionTimbradoCentralTresCx { get; set; }
        public int? DuracionContestoCentralTresCx { get; set; }
        public int? DuracionTimbradoCentralRingover { get; set; }
        public int? DuracionContestoCentralRingover { get; set; }
        public int? IdLlamadaCentralLWCC { get; set; }
        public int? IdLlamadaCentralTresCx { get; set; }
        public int? IdLlamadaCentralRingover { get; set; }
        public DateTime? FechaInicioLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public DateTime? FechaInicioLlamadaTresCXIntegra { get; set; }
        public DateTime? FechaFinLlamadaTresCXIntegra { get; set; }
        public DateTime? FechaInicioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public DateTime? FechaInicioLlamadaRingover { get; set; }
        public DateTime? FechaFinLlamadaRingover { get; set; }
        public string? NombreGrabacionIntegra { get; set; }
        public string? NombreGrabacionTresCXIntegra { get; set; }
        public string? NombreGrabacionTresCX { get; set; }
        public string? NombreGrabacionRingover { get; set; }
        public string? EstadoLlamadaIntegra { get; set; }
        public string? SubEstadoLlamadaIntegra { get; set; }
        public string? EstadoLlamadaTresCXIntegra { get; set; }
        public string? SubEstadoLlamadaTresCXIntegra { get; set; }
        public string? EstadoLlamadaTresCX { get; set; }
        public string? SubEstadoLlamadaTresCX { get; set; }
        public string? EstadoLlamadaRingover { get; set; }
        public string? SubEstadoLlamadaRingover { get; set; }
        public string? Webphone { get; set; }
        public string? WebphoneTresCx { get; set; }
        public string? WebphoneRingover { get; set; }
        public bool OtroMedio { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
    }
    public class OportunidadLogReporteDTO
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
        public string? UrlGrabacion2 { get; set; }
        public string? WebphoneGrabacion { get; set; }
        public string? TelefonoDestinoReal { get; set; }
        public string? TelefonoDestino { get; set; }
        public string? AnexoCentral { get; set; }
        public string? OrigenLlamada { get; set; }
        public string? TranscripcionLlamada { get; set; }
        public bool? esLlamadaCalificada { get; set; }
        public bool? esLlamadaTranscrita { get; set; }

    }
    public class OportunidadLogReporteSeguimientoNWDetalleDTO
    {
        public int IdOportunidadLog { get; set; }
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
        public List<LlamadaIntegraDTO> LlamadasIntegra { get; set; } = new List<LlamadaIntegraDTO>();
        public List<Llamada3CXDTO> Llamadas3CX { get; set; } = new List<Llamada3CXDTO>();
    }
    public class OportunidadLogReporteSeguimientoNWDetalleAlternoDTO
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
        public List<LlamadaIntegra3cxDTO> LlamadasIntegra { get; set; } = new List<LlamadaIntegra3cxDTO>();
        public List<LlamadaIntegra3cxDTO> Llamadas3CX { get; set; } = new List<LlamadaIntegra3cxDTO>();
        public List<LlamadaIntegra3cxDTO> LlamadasRingover { get; set; } = new List<LlamadaIntegra3cxDTO>();
    }
    public class OportunidadLogReporteSeguimientoDetalleDTO
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
    public class OportunidadLogReporteSeguimientoYPersonalDTO
    {
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int? IdActividadDetalle { get; set; }
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
        public int? Duracion { get; set; }
        public string? TiempoDuracion { get; set; }
        public string? TiempoDuracionMinutos { get; set; }
        public string? TiempoDuracion3CX { get; set; }
        public int? IdCentralLLamada { get; set; }
        public int? IdTresCX { get; set; }
        public DateTime? FechaIncioLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public string? NombreGrabacionIntegra { get; set; }
        public string? NombreGrabacionTresCX { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string? EstadoLlamadaIntegra { get; set; }
        public string? SubEstadoLlamadaIntegra { get; set; }
        public string? EstadoLlamadaTresCX { get; set; }
        public string? SubEstadoLlamadaTresCX { get; set; }
        public int IdOportunidadLog { get; set; }
        public int IdOportunidad { get; set; }
        public bool EstadoOportunidadLog { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public int IdContacto { get; set; }
        public string? Webphone { get; set; }
        public string? Personal { get; set; }
    }
    public class OportunidadLogHistorialComentariosDTO
    {
        public int IdOportunidadLog { get; set; }
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
        public string? Personal { get; set; }
        public List<LlamadaIntegraDTO> LlamadasIntegra { get; set; } = new List<LlamadaIntegraDTO>();
        public List<Llamada3CXDTO> Llamadas3CX { get; set; } = new List<Llamada3CXDTO>();
    }
    public class ReporteActividadOcurrenciaDTO
    {
        public int? IdOportunidad { get; set; }
        public int? IdEstadoOcurrencia { get; set; }
        public int? IdFaseOportunidadAnterior { get; set; }
        public int? IdFaseActual { get; set; }
        public DateTime? FechaReal { get; set; }
    }
    public class ReporteSeguimientoNWActividadDTO
    {
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
        public int? TotalEjecutadas { get; set; }
        public int? TotalNoEjecutadas { get; set; }
        public int? TotalAsignacionManual { get; set; }
        public string? EstadoFase { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaPago { get; set; }
        public List<LlamadaIntegraDTO> LlamadasIntegra { get; set; } = new List<LlamadaIntegraDTO>();
        public List<Llamada3CXDTO> Llamadas3CX { get; set; } = new List<Llamada3CXDTO>();
    }
    public class ReporteSeguimientoNWActividadAlternoDTO
    {
        public int IdActividadDetalle { get; set; }
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string? NombreActividad { get; set; }
        public string? NombreOcurrencia { get; set; }
        public string? ComentarioActividad { get; set; }
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

    public class InteraccionAnteriorResumidaDTO
    {
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaSiguienteLlamada { get; set; }
        public string? FaseInicio { get; set; }
        public string? FaseDestino { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
    }

    public class HistorialInteraccionesResponseDTO
    {
        public List<ReporteSeguimientoNWActividadAlternoDTO?> PrimerasInteracciones { get; set; } = new List<ReporteSeguimientoNWActividadAlternoDTO?>();
        public List<InteraccionAnteriorResumidaDTO> InteraccionesAnteriores { get; set; } = new List<InteraccionAnteriorResumidaDTO>();
    }
    public class EstadoFaseOportunidadLogDTO
    {
        public int? IdFaseOportunidad { get; set; }
        public int? IdFaseOportunidadIP { get; set; }
        public int? IdFaseOportunidadPF { get; set; }
        public int? IdFaseOportunidadIC { get; set; }
    }
    public class UltimoOportunidadLogDTO
    {
        public DateTime? Fecha_Log { get; set; }
        public DateTime? FechaCambioFase { get; set; }
        public DateTime? FechaCambioFaseIs { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public int? IdCentroCosto { get; set; }
        public DateTime? FechaCambioFaseAnt { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnt { get; set; }
        public int? CambioFaseAsesor { get; set; }
        public int? CicloRn2 { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }
    public class OportunidadLogFinalizarActividadDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidadAnt { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdActividadDetalle { get; set; }
        public int? IdOcurrencia { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public string Comentario { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdFaseOportunidadIp { get; set; }
        public int? IdFaseOportunidadIc { get; set; }
        public DateTime? FechaEnvioFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadPf { get; set; }
        public DateTime? FechaPagoFaseOportunidadIc { get; set; }
        public bool? FasesActivas { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
        public int? IdFaseOportunidadPf { get; set; }
        public string CodigoPagoIc { get; set; }
        public int? IdAsesorAnt { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public DateTime? FechaFinLog { get; set; }
        public DateTime? FechaCambioFase { get; set; }
        public bool? CambioFase { get; set; }
        public DateTime? FechaCambioFaseIs { get; set; }
        public bool? CambioFaseIs { get; set; }
        public DateTime? FechaCambioFaseAnt { get; set; }
        public DateTime? FechaCambioAsesor { get; set; }
        public DateTime? FechaCambioAsesorAnt { get; set; }
        public int? CambioFaseAsesor { get; set; }
        public int? CicloRn2 { get; set; }
        public int? IdSubCategoriaDato { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
    }


    public class OportunidadLogRevertirDTO
    {
        public int Id { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdContacto { get; set; }
        public DateTime? FechaLog { get; set; }
    }

    public class ObtenerDetalleOportunidadDTO
    {
        public string FaseInicio { get; set; }
        public string FaseDestino { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string Estado { get; set; }
    }
}
