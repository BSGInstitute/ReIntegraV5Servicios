namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TroncalPgeneralDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int? IdTroncalPartner { get; set; }
        public int Duracion { get; set; }
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int? IdBusqueda { get; set; }
        public int? IdMigracion { get; set; }
    }
    public class TroncalPgeneralFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdSubArea { get; set; }
    }
    public class LocacionTroncalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int? CodigoBS { get; set; }
        public string DenominacionBS { get; set; }

    }
    public class TroncalPGeneralSubAreaCodigoDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? IdSubArea { get; set; }
        public string? Codigo { get; set; }
    }
}
