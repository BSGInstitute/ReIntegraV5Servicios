namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class AgendaTabDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool VisualizarActividad { get; set; }
        public bool CargarInformacionInicial { get; set; }
        public int Numeracion { get; set; }
        public bool ValidarFecha { get; set; }
        public string? CodigoAreaTrabajo { get; set; }
    }
    public class AgendaTabAlternoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? CodigoAreaTrabajo { get; set; }
    }

    public class PruebaActividadRealizadaDTO
    {
        public int Id { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Contacto { get; set; }
        public string CodigoFase { get; set; }
        public int IdFaseFinal { get; set; }
        public string NombreTipoDato { get; set; }
        public string Origen { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int Duracion { get; set; }
        public int UnicoTimbrado { get; set; }
        public int UnicoContesto { get; set; }
        public string UnicoEstadoLlamada { get; set; }
        public DateTime? UnicoFechaLlamada { get; set; }
        public string UnicoClasificacion { get; set; }
        public string Actividad { get; set; }
        public string Ocurrencia { get; set; }
        public string Comentario { get; set; }
        public string Asesor { get; set; }
        public int IdContacto { get; set; }
        public int IdOportunidad { get; set; }
        public int IdCategoria { get; set; }
        public string ProbActual { get; set; }
        public string FaseMaxima { get; set; }
        public string FaseInicial { get; set; }
        public int TotalOportunidades { get; set; }
        public string NombreCategoria { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string TiempoLlamadas { get; set; }
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string NumeroLlamadas { get; set; }
        public string EstadoLlamada { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string EstadoClasificacion { get; set; }
        public int? IdTcentralLLamada { get; set; }
        public string NombreGrupo { get; set; }
        public DateTime? FechaLlamadaFin { get; set; }
        public string SubEstadoLlamada { get; set; }
        public DateTime? FechaIncioLlamadaTresCX { get; set; }
        public DateTime? FechaFinLlamadaTresCX { get; set; }
        public string SubEstadoLlamadaTresCX { get; set; }
        public string EstadoLlamadaTresCX { get; set; }
        public int? IdTresCX { get; set; }
        public int? TiempoTimbradoTresCx { get; set; }
        public int? TiempoContestoTresCx { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string NombreGrabacionTresCX { get; set; }
        public string NombreGrabacionIntegra { get; set; }
    }
}
