namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GastoFinancieroCronogramaDetalleDTO
    {
        public int Id { get; set; }
        public int IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
    }

    public class CronogramaDetalleEnvioDTO
    {
        public int Id { get; set; }
        public int? IdGastoFinancieroCronograma { get; set; }
        public int NumeroCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
        public string? Usuario { get; set; }
    }

}
