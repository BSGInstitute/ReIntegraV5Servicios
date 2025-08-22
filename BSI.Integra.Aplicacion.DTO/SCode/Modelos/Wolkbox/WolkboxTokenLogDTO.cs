namespace BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox
{
    public class WolkboxTokenLogDTO
    {
        public int IdWolkboxToken { get; set; }
        public int IdPersonal { get; set; }
        public string AgentId { get; set; }
        public string Method { get; set; }
        public string UrlApi { get; set; }
        public string QueryParams { get; set; }
        public string Body { get; set; }
        public string StatusCode { get; set; }
        public string Response { get; set; }
    }
}
