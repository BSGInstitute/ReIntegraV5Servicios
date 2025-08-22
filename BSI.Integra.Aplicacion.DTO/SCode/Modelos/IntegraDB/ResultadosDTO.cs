namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ResultadosDTO
    {
        public int IdAsesor { get; set; }
        public int Resultado { get; set; }
    }
    public class OportunidadProgramadaManualDTO
    {
        public int IdOportunidad { get; set; }
    }
    public class ResultadoFechaCompromiso
    {
        public int NroFechaCompromiso { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaCompromiso { get; set; }
    }
    public class AgendaAtcCompromiso
    {
        public int Version { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaCompromiso { get; set; }
    }
}
