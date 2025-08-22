namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteMensajesWhatsAppPorAreaFiltrosDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdArea { get; set; }
    }

    public class ReporteWhatsAppMasivoFiltrosDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdPersonal { get; set; }
        public int IdPais { get; set; }
    }

}
