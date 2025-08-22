namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ActividadDetalleDTO
    {
        public int Id { get; set; }
        public int? IdActividadCabecera { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        public string? Comentario { get; set; }
        public int? IdAlumno { get; set; }
        public string? Actor { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentralLlamada { get; set; }
        public string? RefLlamada { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
    }
    public class CompuestoActividadEjecutadaDTO
    {
        public int TotalOportunidades { get; set; }
        public int Id { get; set; }
        public string CentroCosto { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public Nullable<int> Duracion { get; set; }
        public string Actividad { get; set; }
        public string Ocurrencia { get; set; }
        public string Comentario { get; set; }
        public string Asesor { get; set; }
        public int IdContacto { get; set; }
        public int IdOportunidad { get; set; }

        public string ProbActual { get; set; }
        public string Ca_nombre { get; set; }
        public int IdCategoria { get; set; }

        public string TiempoLlamadas { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseInicial { get; set; }
        public string NumeroLlamadas { get; set; }

        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public string UnicoTimbrado { get; set; }
        public string UnicoContesto { get; set; }
        public string UnicoEstadoLlamada { get; set; }
        public DateTime? FechaLlamadaIntegra { get; set; }
        public string Estado { get; set; }
        public string EstadoClasificacion { get; set; }
        public string UnicoClasificacion { get; set; }
        public DateTime? UnicoFechaLlamada { get; set; }
        public int MinutosIntervale { get; set; }
        public int MinutosTotalTimbrado { get; set; }
        public int MinutosTotalContesto { get; set; }
        public int MinutosTotalPerdido { get; set; }
        public int MayorTiempo { get; set; }
        public string TiemposTresCX { get; set; }
        public string NombreGrupo { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public string SubEstadoLlamadaIntegra { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaLlamadaFin { get; set; }
        public int? IdTresCX { get; set; }
        public int? IdLlamada { get; set; }
        public int? TiempoTimbradoTresCx { get; set; }
        public int? TiempoContestoTresCx { get; set; }
        public string EstadosTresCX { get; set; }
        public string FechaLlamada { get; set; }
        public int TotalEjecutadas { get; set; }
        public int TotalNoEjecutadas { get; set; }
        public int TotalAsignacionManual { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string NombreGrabacionIntegra { get; set; }

    }
}
