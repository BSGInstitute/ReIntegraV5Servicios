namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatDetalleIntegraDTO
    {
        public int Id { get; set; }
        public int? IdInteraccionChatIntegra { get; set; }
        public string? NombreRemitente { get; set; }
        public string IdRemitente { get; set; } = null!;
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public int? IdChatDetalleIntegraArchivo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ChatDetalleIntegraComboDTO
    {
        public int Id { get; set; }
        public string? NombreRemitente { get; set; }
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class HistorialChatRecibidosDTO
    {
        public int IdInteraccionChat { get; set; }
        public string NombreRemitente { get; set; }
        public string Ubicacion { get; set; }
        public string Mensaje { get; set; }
        public int IdAsesor { get; set; }
        public DateTime? Fecha { get; set; }
        public string Chatsession { get; set; }
    }

    public class HistorialChatDetalleIntegraDTO
    {
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdRemitente { get; set; }
    }
    public class HistorialChatEntradaDTO
    {
        public int valor { get; set; }
    }

    public class DatosSesionChatDTO
    {
        public int Id { get; set; }
        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdEstadoChat { get; set; }
        public string IdChatSession { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool? Leido { get; set; }
        public bool? EsAcademico { get; set; }
        public bool? EsSoporteTecnico { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
    public class DatosSesionChatComercialDTO
    {
        public int Id { get; set; }
        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int? IdEstadoChat { get; set; }
        public string IdChatSession { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool? Leido { get; set; }
        public bool? EsAcademico { get; set; }
        public bool? EsSoporteTecnico { get; set; }
        public int? IdMatriculaCabecera { get; set; }
    }
}
