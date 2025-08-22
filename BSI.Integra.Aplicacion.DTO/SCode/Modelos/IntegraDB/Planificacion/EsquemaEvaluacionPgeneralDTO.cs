
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class EsquemaEvaluacionPgeneralDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsquemaPredeterminado { get; set; }
    }
    public class EsquemaEvaluacionPgeneralAsociadoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdPgeneral { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? EsquemaPredeterminado { get; set; }
        public string Esquema { get; set; }
        public List<int> ListadoModalidad { get; set; }
        public string ModalidadMostrar { get; set; }
        public List<int> ListadoProveedor { get; set; }
    }
}
