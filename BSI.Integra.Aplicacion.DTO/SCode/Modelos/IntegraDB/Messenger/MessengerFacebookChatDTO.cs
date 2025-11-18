namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger
{
    public class ResumenMessengerFacebookChatDTO
    {
        public string IdentificadorAmbitoPagina { get; set; } = string.Empty;
        public int? IdAlumno { get; set; }
        public string NombreAlumno { get; set; } = string.Empty;
        public string NombrePagina { get; set; } = string.Empty;
        public string TipoMensaje { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaMensaje { get; set; }
    }

    public class ChatMessengerFacebookDTO
    {
        public string TipoMensaje { get; set; }
        public string Contenido { get; set; }
        public string TipoAdjunto { get; set; }
        public string URLAdjunto { get; set; }
        public string FechaEvento { get; set; }
        public bool EsMensajeSaliente { get; set; }
        public string NombrePagina { get; set; }
        public int IdMessengerConfiguracionPagina { get; set; }
    }

    public class ObtenerHistorialChatPorPSIDRequestDTO
    {
        public string IdentificadorAmbitoPagina { get; set; }
    }

    public class ValidarExistePSIDResponse
    {
        public bool Existe { get; set; }
    }

    public class EnviarMensajeTextoRequest
    {
        public string PSID { get; set; } = null!;
        public int IdMessengerConfiguracionPagina { get; set; }
        public string TipoMensaje { get; set; }
        public string? Contenido { get; set; }
        public string? TipoAdjunto { get; set; }
        public string? URLAdjunto { get; set; }
        public string? MId { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdPersonal { get; set; }
        public string? UsuarioCreacion { get; set; }
    }
    public class EnviarMensajeResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string? MetaMessageId { get; set; }
        public string? RawResponse { get; set; } = string.Empty;
    }


}
