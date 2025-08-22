namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteChatLogDTO
    {
        public int IdPGeneral { get; set; }
        public string Area { get; set; }
        public string Asesor { get; set; }
        public int Chats { get; set; }
        public int Promedio { get; set; }
        public int ErrorSignal { get; set; }
        public int ErrorCarga { get; set; }
        public int ErrorCrear { get; set; }
        public decimal Tiempo { get; set; }
        public int? Activo { get; set; }
        public int? NroChatsActivos { get; set; }
    }

}
