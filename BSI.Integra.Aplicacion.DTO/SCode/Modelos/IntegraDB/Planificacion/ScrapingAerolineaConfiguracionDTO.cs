namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ScrapingAerolineaConfiguracionDTO
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? NroGrupoSesion { get; set; }
        public int? NroGrupoCronograma { get; set; }
        public int IdScrapingAerolineaEstadoConsulta { get; set; }
        public int IdCiudadOrigen { get; set; }
        public int IdCiudadDestino { get; set; }
        public DateTime FechaIda { get; set; }
        public DateTime FechaRetorno { get; set; }
        public decimal? PrecisionIda { get; set; }
        public int? NroFrecuencia { get; set; }
        public string? TipoFrecuencia { get; set; }
        public string? TipoVuelo { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFur { get; set; }
        public decimal? PrecisionRetorno { get; set; }
        public bool? TienePasajeComprado { get; set; }
    }
}