namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wolkbox.WolkboxAgent
{
    public class WolkboxMarcarDTO
    {
        public string customer_phone { get; set; }
        public string? customer_id { get; set; }
        public string? customer_name { get; set; }
    }
    public class ResponseMarcarDTO
    {
        public string? code { get; set; }
        public string? error { get; set; }
        public string? msg { get; set; }
        public ConnIdDTO? data { get; set; }
    }
    public class ConnIdDTO
    {
        public string? conn_id { get; set; }
    }
}
