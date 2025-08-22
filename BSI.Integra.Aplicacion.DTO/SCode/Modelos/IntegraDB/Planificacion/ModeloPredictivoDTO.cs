
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ModeloPredictivoProgramaDTO
    {
        public List<ModeloPredictivoIndustriaDTO> Industria { get; set; }
        public List<ModeloPredictivoCargoDTO> Cargo { get; set; }
        public List<ModeloPredictivoFormacionDTO> Formacion { get; set; }
        public List<ModeloPredictivoTrabajoDTO> Trabajo { get; set; }
        public List<ModeloPredictivoCategoriaDatoDTO> CategoriaOrigen { get; set; }
        public List<ModeloPredictivoEscalaDTO> Escala { get; set; }
        public List<ModeloPredictivoTipoDatoDTO> TipoDato { get; set; }
        public ModeloPredictivoInterceptoDTO Intercepto { get; set; }

    }
}
