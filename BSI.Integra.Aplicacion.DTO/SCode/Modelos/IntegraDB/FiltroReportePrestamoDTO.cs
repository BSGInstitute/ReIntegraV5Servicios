namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroReportePrestamoDTO
    {
        public int IdEntidadFinanciera { get; set; }
        public int IdGastoFinancieroCronograma { get; set; }
    }
    public class ReporteDePrestamoDTO
    {
        public int NumeroCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public decimal TotalCuota { get; set; }
        public string NombreMoneda { get; set; }
    }
    public class PrestamoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
    }
}




