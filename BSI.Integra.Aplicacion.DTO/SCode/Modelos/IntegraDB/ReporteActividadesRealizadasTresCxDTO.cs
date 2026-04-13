namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteRealizadaDataTresCxDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public int? IdLlamadaWebphoneCruceCentralTresCx { get; set; }
        public string? NombreGrabacionTresCx { get; set; }
        public string Webphone { get; set; }
        public int? IdTresCX { get; set; }
        public int? DuracionContestoTresCx { get; set; }
        public int? DuracionTimbradoTresCx { get; set; }
        public DateTime? FechaInicioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string AnexoCentral { get; set; }
        public string TelefonoDestinoReal { get; set; }
        public string TelefonoDestino { get; set; }
        public string UrlGrabacionTresCX { get; set; }
    }
    public class ReporteRealizadaDataTresCxAlternoDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public int? IdTresCx { get; set; }
        public int? IdRingover { get; set; }
        public int? IdLlamadaWebphone { get; set; }
        public int? DuracionTimbradoIntegra { get; set; }
        public int? DuracionContestoIntegra { get; set; }
        public DateTime? FechaInicioLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaIntegra { get; set; }
        public DateTime? FechaInicioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public DateTime? FechaInicioLlamadaRingover { get; set; }
        public DateTime? FechaFinLlamadaRingover { get; set; }
        public string? NombreGrabacionIntegra { get; set; }
        public string? NombreGrabacionCentral { get; set; }
        public string? NombreGrabacionTresCx { get; set; }
        public string? NombreGrabacionRingover { get; set; }
        public string? WebphoneIntegra { get; set; }
        public string? WebphoneTresCx { get; set; }
        public string? WebphoneRingover { get; set; }
        public int? DuracionTimbradoCentral { get; set; }
        public int? DuracionContestoCentral { get; set; }
        public int? DuracionTimbradoTresCx { get; set; }
        public int? DuracionContestoTresCx { get; set; }
        public int? DuracionTimbradoRingover { get; set; }
        public int? DuracionContestoRingover { get; set; }
        public DateTime? FechaInicioLlamadaCentral { get; set; }
        public DateTime? FechaFinLlamadaCentral { get; set; }
        public int? IdCentral { get; set; }
        public int? IdCentraTresCX { get; set; }
        public int? IdCentraRingover { get; set; }
        public string? EstadoLlamadaCentral { get; set; }
        public string? SubEstadoLlamadaCentral { get; set; }
        public string? EstadoLlamadaTresCX { get; set; }
        public string? SubEstadoLlamadaTresCX { get; set; }
        public string? EstadoLlamadaRingover { get; set; }
        public string? SubEstadoLlamadaRingover { get; set; }
        public string? TelefonoDestinoReal { get; set; }
        public string? TelefonoDestino { get; set; }
        public string? AnexoCentral { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }
    }
    public class ReporteActividadRealizadaDTO
    {
        public int IdActividad { get; set; }
        public int IdOportunidadSeguimiento { get; set; }

        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool EsOtroMedio { get; set; }
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
        public int? DiferenciaFechaActualFechaRealmin { get; set; }
    }
    public class CompuestoActividadesRealizadasAlternoTresCxDTO
    {
        public int IdActividad { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasIntegra { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasTresCx { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasRingover { get; set; }
        public List<InformacionLlamadaAlternoDTO> LlamadasIntegraTresCx { get; set; }
    }
    public class CompuestoActividadRealizadaDTO
    {
        public int IdActividad { get; set; }
        public int IdOportunidadSeguimiento { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public int IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }
        public int DiferenciaFechaActualFechaRealmin { get; set; }
        public List<InformacionLlamadaAlternoDTO> Llamadas { get; set; }
    }
    public class ProcesadoDataActividadesRealizadasAlternoTresCxDTO
    {
        public int IdActividad { get; set; }
        public int IdOportunidadSeguimiento {  get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombreCompletoContacto { get; set; }
        public string CodigoFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string NombreOrigen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime FechaReal { get; set; }
        public string NombreActividadCabecera { get; set; }
        public string NombreOcurrencia { get; set; }
        public string ComentarioActividad { get; set; }
        public string NombreCompletoAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public string ProbabilidadActual { get; set; }
        public string CodigoFaseOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string NombreGrupo { get; set; }
        public bool EstadoSeguimientoWhatsApp { get; set; }
        public bool OtroMedio { get; set; }

        //public List<TiempoTresCXDTO> TiemposDuracionLlamadas { get; set; }
        public double MinutosTotalIntervaleLlamadas { get; set; }
        public double MinutosIntervalo { get; set; }
        public double MinutosTotalTimbrado { get; set; }
        public double MinutosTotalContesto { get; set; }
        public double MinutosTotalPerdido { get; set; }
        public double MayorTiempo { get; set; }
        public List<TiempoTresCXDTO> Tiempos { get; set; }
        public List<EstadoTresCXDTO> Estados { get; set; }
        public List<FechaLlamadaDTO> FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public double MinutosPerdidosOcurrencia { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public List<NombreGrabacionAlternoDTO> NombreGrabacion { get; set; } = new List<NombreGrabacionAlternoDTO>();
        public bool ExisteLlamadaExitosa { get; set; }
        public bool TieneLlamadaHumano { get; set; }
        public int DiferenciaFechaActualFechaRealmin { get; set; }

    }

    public class ProcesadoDataChatAsistenteVirtualDTO
    {
        public int IdPersonal { get; set; }
        public string Personal { get; set; }
        public int IdHiloChat { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public string NombreTipoDato { get; set; }
        public DateTime FechaChat { get; set; }

    }
}
