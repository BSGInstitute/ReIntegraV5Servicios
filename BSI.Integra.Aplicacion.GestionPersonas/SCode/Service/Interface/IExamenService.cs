
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IExamenService
    {
        List<int> ObtenerIdGruposPorEvaluacion(int idEvaluacion);
        bool ActualizarFactorComponente(FactorComponenteDTO Json);
        List<FactorComponenteDTO> ObtenerComponentePorEvaluacion(int idEvaluacion);
        List<ExamenVDTO> ObtenerComponentesPorEvaluacion(int idEvaluacion);
    }
}
