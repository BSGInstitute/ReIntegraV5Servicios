namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CiudadDepartamentoPaiDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int IdDepartamentoPais { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class CodigoCiudadDTO
    {
        public string Codigo { get; set; }
    }
}
