namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CiudadDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? LongCelularAlterno { get; set; }
    }
    public class CiudadAlternoDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public int? LongCelularAlterno { get; set; }
        public string NombrePais { get; set; }
    }
    public class CiudadComboDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
    }

    public class CiudadEnvioDTO
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public int? LongCelularAlterno { get; set; }
        public string? nombrePais { get; set; }
        public string? Usuario { get; set; } = null!;
    }
    public class CiudadColoniaDTO
    {
        public string? d_codigo { get; set; }
        public string? d_asenta { get; set; }
        public string? d_tipo_asenta { get; set; }
        public string? D_mnpio { get; set; }
        public string? d_estado { get; set; }
        public string? d_ciudad { get; set; }
        public string? d_CP { get; set; }
        public string? c_estado { get; set; }
        public string? c_oficina { get; set; }
        public string? c_CP { get; set; }
        public string? c_tipo_asenta { get; set; }
        public string? c_mnpio { get; set; }
        public string? id_asenta_cpcons { get; set; }
        public string? d_zona { get; set; }
        public string? c_cve_ciudad { get; set; }
    }
    public class RegionCiudadComboDTO
    {
        public int Id { get; set; }
        public int IdCiudad { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
    }

    public class CiudadDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int Codigo { get; set; }
    }
    public class CiudadMultipleDTO
    {
        public int LongCelular { get; set; }
        public int LongTelefono { get; set; }
        public int IdPais { get; set; }
        public List<int> IdsCiudades { get; set; }
    }
}
