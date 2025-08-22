namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class OperadorComparacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Simbolo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class OperadorComparacionComboNombreDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Modalidad { get; set; }
        public string Codigo { get; set; }
        public bool? ConsiderarEnvioAutomatico { get; set; }
        public string TipoPersonal { get; set; }
    }
    public class OperadoresComparacionDTO
    {
        public int Id { get; set; }
        public string Simbolo { get; set; }

    }
    public class OperadoresComparacionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }
}
