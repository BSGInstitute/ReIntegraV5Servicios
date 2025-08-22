namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteTasaContactoDTO
    {
        public int TotalLlamadas { get; set; }
        public int TotalLlamadasEjecutadas { get; set; }
        public int TotalLlamadasManual { get; set; }
        public int TotalLlamadasEjecutadasConLlamada { get; set; }
    }
    public class ReporteTasaContactoConySinLlamadaDTO
    {
        public int CambiosFaseConLlamada { get; set; }
        public int CambiosFaseTotal { get; set; }
        public int CambiosFaseSinLlamada { get; set; }
        public int? CambiosFaseOCotroMedio { get; set; }
        public int CambiosFaseOCconLlamada { get; set; }
        public int CambiosFaseOCsinLlamada { get; set; }
    }
    public class PreReporteTasaContactoDTO
    {
        public int IdOcurrencia { get; set; }
        public int IdEstadoOcurrencia { get; set; }
    }
    public class TasaContactoEjecutadoDTO
    {
        public int CantidadTotal { get; set; }
        public int CantidadTotalEjecutada { get; set; }
        public int CantidadTotalManual { get; set; }

    }


}
