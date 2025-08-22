namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatReporteDTO
    {
        public string CentroCosto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Areas { get; set; }
        public string Asesor { get; set; }
        public string? Pais { get; set; }
        public int Desglose { get; set; }
    }
    public class ChatAgrupadoDTO
    {
        public string Pais { get; set; }
        public DateTime Fecha { get; set; }
        public List<ChatIntegraDetalleDTO> Detalle { get; set; }
    }
    public class ChatIntegraDetalleDTO
    {
        public string Fecha { get; set; }
        public List<ChatIntegraSubDetalleDTO> Detalle { get; set; }
    }
    public class ChatIntegraSubDetalleDTO
    {
        public string Asesor { get; set; }
        public string Area { get; set; }
        public int Oportunidades { get; set; }
        public int Chats { get; set; }
        public int Promedio { get; set; }
        public int PalabrasVisitante { get; set; }
        public int Logueados { get; set; }
        public int ClickEmpezar { get; set; }
        public int Atendidos { get; set; }
        public int NoAtendidos { get; set; }
        public decimal ClienteTiempoEspera { get; set; }
    }
    public class ReporteChatIntegraDTO
    {
        public string Asesor { get; set; }
        public string Area { get; set; }
        public DateTime Fecha { get; set; }
        public int Oportunidades { get; set; }
        public int Chats { get; set; }
        public int Promedio { get; set; }
        public int PalabrasVisitante { get; set; }
        public int Logueados { get; set; }
        public int ClickEmpezar { get; set; }
        //public int IdPais { get; set; }
        public string Pais { get; set; }
        public int Atendidos { get; set; }
        public decimal ClienteTiempoEspera { get; set; }
        //public int NoAtendidos { get; set; }
    }

}


