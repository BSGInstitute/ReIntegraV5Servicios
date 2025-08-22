namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentacionComercialPwDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Modalidad { get; set; } = null!;
        public int? IdPais { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentacionComercialPwComboDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
    }
}
