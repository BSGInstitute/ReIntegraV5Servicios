namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoEnviadoWebPwDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
