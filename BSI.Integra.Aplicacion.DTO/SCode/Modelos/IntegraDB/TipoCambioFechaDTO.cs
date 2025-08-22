namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoCambioFechaDTO
    {
        public double Cambio { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class TipoCambioReporteDTO
    {
        public int Id { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public double MonedaADolar { get; set; }
        public double DolarAMoneda { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdPeriodo { get; set; }
        public string NombreUsuario { get; set; }
    }
}
