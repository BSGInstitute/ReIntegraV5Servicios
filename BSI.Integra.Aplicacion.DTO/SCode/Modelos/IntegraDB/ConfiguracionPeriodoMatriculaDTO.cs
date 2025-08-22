namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionPeriodoMatriculaRecibidoDTO
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
