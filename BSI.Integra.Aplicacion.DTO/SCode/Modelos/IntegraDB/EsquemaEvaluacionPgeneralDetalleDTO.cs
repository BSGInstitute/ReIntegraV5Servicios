namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EsquemaEvaluacionPgeneralDetalleDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? UrlArchivoInstrucciones { get; set; }
        public int? IdProveedor { get; set; }
    }
    public class EsquemaEvaluacionPgeneralDetalleCompuestoDTO
    {
        public int Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string NombreCriterioEvaluacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? UrlArchivoInstrucciones { get; set; }
        public int? IdProveedor { get; set; }
    }
    public class DetalleEsquemaAsignadoDTO
    {
        public int IdCriterioEvaluacion { get; set; }
        public List<EsquemaEvaluacionPgeneralDetalleCompuestoDTO> Detalle { get; set; }
    }
}
