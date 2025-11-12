namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger
{
    public class ResumenMessengerFacebookChatDTO
    {
        public string PSID { get; set; } = string.Empty;
        public int? IdAlumno { get; set; }
        public string NombreAlumno { get; set; } = string.Empty;
        public string NombrePaginaOrigen { get; set; } = string.Empty;
        public string UltimoMensaje { get; set; } = string.Empty;
        public DateTime FechaUltimoMensaje { get; set; }
    }

}
