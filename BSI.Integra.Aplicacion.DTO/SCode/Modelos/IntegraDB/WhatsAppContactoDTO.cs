namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ValidarNumerosWhatsAppDTO
    {
        public string blocking { get; set; }
        public List<string> contacts { get; set; }
    }
    public class numerosValidos
    {
        public contacts[] contacts { get; set; }
        public meta meta { get; set; }
        public errors[] errors { get; set; }
    }
    public class contacts
    {
        public string input { get; set; }
        public string status { get; set; }
        public string wa_id { get; set; }
    }
}
