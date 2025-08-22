namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppConfiguracionDTO
    {
        public int Id { get; set; }
        public string UrlWhatsApp { get; set; }
        public string IpHost { get; set; }
        public string Numero { get; set; }
        public string VName { get; set; }
        public string Certificado { get; set; }
        public string IdPais { get; set; }
    }

    public class WhatsAppHostDatosDTO
    {
        public int Id { get; set; }
        public string UrlWhatsApp { get; set; }
        public string IpHost { get; set; }
        public int IdPais { get; set; }
    }
}
