namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EmpresaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? PaginaWeb { get; set; }
        public string? Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        public string? IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? Ciiu { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public string usuario { get; set; }
        public string? Municipio { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? EstadoLugar { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
    }

    public class EmpresaObtenerDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Ruc { get; set; }
        public int? IdTipoIdentificador { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? PaginaWeb { get; set; }
        public string? Email { get; set; }
        public int? Trabajadores { get; set; }
        public double? NivelFacturacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdRegion { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdIndustria { get; set; }
        public string? IdTipoEmpresa { get; set; }
        public int? IdTamanio { get; set; }
        public int? IdCodigoCiiuIndustria { get; set; }
        public string? Municipio { get; set; }
        public int? IdMunicipio { get; set; }
        public string? EstadoLugar { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? CodigoPostal { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class EmpresaFiltroDTO
    {
        public List<EmpresaObtenerDTO> Data { get; set; }
        public int Total { get; set; }
    }
    public class CodigoCiiuIndustriaComboDTO
    {
        public int Id { get; set; }
        public string CIIU { get; set; } = null!;
        public int IdIndustria { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
