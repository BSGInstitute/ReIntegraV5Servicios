namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EmpresaAutorizadaDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string? Ruc { get; set; }
        public string? Direccion { get; set; }
        public string? Central { get; set; }
        public bool Activo { get; set; }
        public int IdPais { get; set; }
        public string? Pais { get; set; }
        public string? NombreComercial { get; set; }
    }
    public class EmpresaAutorizadaComboDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string Ruc { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Central { get; set; } = null!;
    }
    public class EmpresaAutorizadaComboPaisDTO
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string IdPais { get; set; } = null!;
    }
}
