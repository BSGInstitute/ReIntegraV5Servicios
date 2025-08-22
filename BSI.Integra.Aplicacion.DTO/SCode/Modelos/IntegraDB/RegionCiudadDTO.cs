namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegionCiudadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCiudad { get; set; }
        public int idPais { get; set; }
        public int? CodigoBS { get; set; }
        public int? DenominacionBS { get; set; }
        public string? NombreCorto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }

    }
    public class ComboRegionCiudadDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCiudad { get; set; }
    }
    public class RegionCiudadDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CodigoBS { get; set; }
        public int DenominacionBS { get; set; }
        public string NombreCorto { get; set; }
    }
    public class RegionCiudadEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CodigoBS { get; set; }
        public int DenominacionBS { get; set; }
        public string NombreCorto { get; set; }
        public string Usuario { get; set; } = null!;
    }
    public class RegionCiudadPanelDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? CodigoBS { get; set; }
        public string? DenominacionBS { get; set; }
        public string? NombreCorto { get; set; }
        public string? Total { get; set; }
    }
    public class RegionCiudadPanelDTO2
    {
        public int idPais { get; set; }
        public string nombrePais { get; set; }
        public int idCiudad { get; set; }
        public string nombreCiudad { get; set; }
        public int idRegion { get; set; }
        public string nombreRegion { get; set; }
        public string? CodigoBS { get; set; }
        public string? DenominacionBS { get; set; }
        public string? NombreCorto { get; set; }
    }
}