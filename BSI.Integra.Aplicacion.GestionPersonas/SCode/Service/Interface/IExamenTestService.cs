
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IExamenTestService
    {
        public IEnumerable<ExamenTestResumidoDTO> Obtener();
        public (IEnumerable<FormulaPuntajeDTO>, IEnumerable<ComboDTO>, List<FiltroDTO>) ObtenerCombosExamenTest();
        public (List<EvaluacionAgrupadaComponenteDTO>, IEnumerable<ComboDTO>, IEnumerable<ComboDTO>) ObtenerEvaluacionEditar(int IdEvaluacion);

        public List<CentilDTO> ObtenerCentilEvaluacion(int idExamenTest);
    }
}
