namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TMKMailDataDTO
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        //Atributos dependientes de libreria Mandril
        public List<Mandrill.Models.EmailAttachment> AttachedFiles { get; set; }
        public List<Mandrill.Models.Image> EmbeddedFiles { get; set; }
        public string RemitenteC { get; set; }
    }
    public class TMKMensajeIdDTO
    {
        public string MensajeId { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; }
    }

    public class EstadoCorreoSmtpDTO
    {
        public string codigo { get; set; }
        public string respuesta { get; set;}
    }

}
