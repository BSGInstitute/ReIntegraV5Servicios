namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CompetidorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int DuracionCronologica { get; set; }
        public int CostoNeto { get; set; }
        public int Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdInstitucionCompetidora { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int IdAeaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCategoria { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class CompetidorComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class CompetidorOportunidadAgendaDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public string Nombre { get; set; } = null!;
        public int DuracionCronologica { get; set; }
        public int CostoNeto { get; set; }
        public int Precio { get; set; }
        public string Categoria { get; set; } = null!;
        public string Empresa { get; set; } = null!;
        public string RegionCiudad { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public int? IdCompetidorVentajaDesventaja { get; set; }
        public string? ContenidoCompetidorVentajaDesventaja { get; set; }
        public int? TipoCompetidorVentajaDesventaja { get; set; }
    }
}
