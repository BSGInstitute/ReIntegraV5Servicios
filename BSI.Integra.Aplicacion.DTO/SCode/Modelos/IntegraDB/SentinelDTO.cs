namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SentinelDTO
    {
        public int Id { get; set; }
        public string Dni { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
    }
    public class SentinelComboDTO
    {
        public int Id { get; set; }
        public string Dni { get; set; } = null!;
    }
    public class SentinelDatosAlumnoAgendaDTO
    {
        public string? Dni { get; set; }
        public int? IdSentinel { get; set; }
        public int? IdAlumno { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Ubigeo { get; set; }
        public string? Distrito { get; set; }
        public string? Provincia { get; set; }
        public string? Departamento { get; set; }
        public string? CIIU { get; set; }
        public string? ActividadEconomica { get; set; }
        public string? Direccion { get; set; }
        public string? SemaforoActual { get; set; }
        public string? SemaforoPrevio { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public string? NombreAlterno { get; set; }
    }
    public class SentinelLineaCreditoDatosAlumnoDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? CnsEntNomRazLn { get; set; }
        public string? TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
    }
    public class SentinelLineaDeudaDatosAlumnoDTO
    {
        public string? TipoDocCPT { get; set; }
        public string? NombreRazonSocial { get; set; }
        public string? Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
    }
    public class SentinelDatosAlumnoDetalleAgendaDTO
    {
        public string? Dni { get; set; }
        public int? IdSentinel { get; set; }
        public int? IdAlumno { get; set; }
        public string? TipoDocumento { get; set; }
        public string? Nombre { get; set; }
        public string? Sexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Ubigeo { get; set; }
        public string? Distrito { get; set; }
        public string? Provincia { get; set; }
        public string? Departamento { get; set; }
        public string? CIIU { get; set; }
        public string? ActividadEconomica { get; set; }
        public string? Direccion { get; set; }
        public string? SemaforoActual { get; set; }
        public string? SemaforoPrevio { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public string? NombreAlterno { get; set; }
        public virtual IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> lineaCredito { get; set; } = new List<SentinelLineaCreditoDatosAlumnoDTO>();
        public virtual IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> lineaDeuda { get; set; } = new List<SentinelLineaDeudaDatosAlumnoDTO>();
    }
    public class SueldoPromedioArgumentosDTO
    {
        public int? IdEmpresa { get; set; }
        public string? Dni { get; set; }
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdTamanioEmpresa { get; set; }
    }
    public class SueldoPromedioDTO
    {
        public int? Valor { get; set; }
        public string? Descripcion { get; set; }
    }
    public class ActualizarSentinelResultadoDTO
    {
        public List<SentinelSdtEstandarItemDTO> DniRuc { get; set; }
        public List<SentinelSdtRepSbsitemDTO> Deuda { get; set; }
        public List<SentinelSdtLincreItemDTO> LineaCredito { get; set; }
        public List<SentinelSdtResVenItemDTO> DatosVencidas { get; set; }
        public SentinelSdtInfGenDTO DatosGenerales { get; set; }
        public List<SentinelRepLegItemDTO> Cargo { get; set; }
        public List<SentinelSdtPoshisItemDTO> PosicionHistoria { get; set; }
    }
    public class SentinelDatosContactoDTO
    {
        public string Dni { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombre { get; set; }
        public string NombreAlterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Ubigeo { get; set; }
        public string Distrito { get; set; }
        public string Provincia { get; set; }
        public string Departamento { get; set; }
        public string Rangosueldo { get; set; }
        public string RucEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Cargo { get; set; }
        public string CIIU { get; set; }
        public string ActividadEconomica { get; set; }
        public string Direccion { get; set; }
        public string SemaforoActual { get; set; }
        public string SemaforoPrevio { get; set; }
        public DateTime? FechaUltimaActualizacion { get; set; }
        public virtual IEnumerable<SentinelLineaCreditoDatosAlumnoDTO> lineaCredito { get; set; } = new List<SentinelLineaCreditoDatosAlumnoDTO>();
        public virtual IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> lineaDeuda { get; set; } = new List<SentinelLineaDeudaDatosAlumnoDTO>();
        public virtual ICollection<SentinelLineaDeudaDatosAlumnoDTO> lineaDeudaVencida { get; set; } = new List<SentinelLineaDeudaDatosAlumnoDTO>();


    }
    public class SentinelDatosCabeceraDTO
    {
        public string Mensaje { get; set; }
        public string Color { get; set; }
    }
    public class SentinelDetalleDTO
    {
        public IList<SentinelSdtEstandarItemDniRucDTO> DniRuc { get; set; }
        public IList<SentinelSdtInfGenDatosGeneralesDTO> DatosGenerales { get; set; }
        public IList<SentinelSdtRepSbsitemLineaDeudaDTO> Deuda { get; set; }
        public IList<SentinelSdtResVenItemDatosVencidosDTO> DatosVencidas { get; set; }
        public IList<AlumnosSentinelLineasCreditoDTO> LineaCredito { get; set; }
        public IList<SentinelSdtPoshisItemPosicionHistoriaDTO> PosicionHistoria { get; set; }
    }
    public class SentinelRespuestaDTO
    {
        public bool Respuesta { get; set; }
        public int IdSentinel { get; set; }
        public bool Estado { get; set; }
    }
    public class SentinelSdtEstandarItemDniRucDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string RazonSocial { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string Semaforos { get; set; }
        public string Score { get; set; }
        public string DeudaTotal { get; set; }
        public string SemanaActual { get; set; }
        public string SemanaPrevio { get; set; }
    }
    public class SentinelSdtResVenItemDatosVencidosDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        public string Fuente { get; set; }
        public decimal? Monto { get; set; }
        public Int32? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
        public string Entidad { get; set; }
    }
    public class SentinelCredencialDTO
    {
        public string? DNI { get; set; }
        public string? Clave { get; set; }
        public long? Servicio { get; set; }
        public string? TipoDocumento { get; set; }
    }
}
