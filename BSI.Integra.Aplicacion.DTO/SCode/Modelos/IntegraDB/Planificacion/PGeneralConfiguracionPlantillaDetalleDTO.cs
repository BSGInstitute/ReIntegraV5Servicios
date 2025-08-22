
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PgeneralConfiguracionPlantillaDetalleDTO
    {
        public int? Id { get; set; }
        public int IdPgeneralConfiguracionPlantilla { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? IdOperadorComparacion { get; set; }
        public decimal? NotaAprobatoria { get; set; }
        public bool DeudaPendiente { get; set; }
        public List<int> EstadosMatricula { get; set; }
        public List<int> SubEstadosMatricula { get; set; }
    }
}
