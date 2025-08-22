namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MatriculaCabeceraDatosCertificadoMensajeDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdPersonalReceptor { get; set; }
        public string Mensaje { get; set; } = null!;
        public string ValorAntiguo { get; set; } = null!;
        public string ValorNuevo { get; set; } = null!;
        public bool EstadoMensaje { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class MatriculaCabeceraDatosCertificadoMensajeComboDTO
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
    }
    public class MatriculaCabeceraDatosCertificadoMensajesDTO
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdPersonalRemitente { get; set; }
        public string? Remitente { get; set; }
        public int? IdPersonalReceptor { get; set; }
        public string? Receptor { get; set; }
        public string? Mensaje { get; set; }
        public string? ValorAntiguo { get; set; }
        public string? ValorNuevo { get; set; }
        public bool EstadoMensaje { get; set; }
        public string Usuario { get; set; }
        public bool? solicitud { get; set; }
        public string MensajeRespuesta { get; set; }
    } 
}
