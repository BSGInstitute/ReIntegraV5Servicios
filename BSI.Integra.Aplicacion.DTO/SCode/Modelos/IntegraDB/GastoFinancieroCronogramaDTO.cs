namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class GastoFinancieroCronogramaDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public string? NombreEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public string? NombreMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicio { get; set; }
    }

    public class CronogramaEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdMoneda { get; set; }
        public decimal CapitalTotal { get; set; }
        public decimal InteresTotal { get; set; }
        public DateTime FechaInicioNueva { get; set; }
        public string Usuario { get; set; }
    }

    public class CronogramaYDetalleDTO
    {
        public CronogramaEnvioDTO Cronograma { get; set; }
        public List<CronogramaDetalleEnvioDTO>  Detalle { get; set; }
        public List<int>? DetalleEliminado { get; set; }
    }



}
