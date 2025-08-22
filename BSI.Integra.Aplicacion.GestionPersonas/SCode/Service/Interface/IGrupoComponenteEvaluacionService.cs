
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IGrupoComponenteEvaluacionService
    {
        List<ComboDTO> ObtenerGruposPorIdEvaluacion(List<int> listaIdGrupo);
        bool ActualizarFactorGrupoComponente(GrupoComponenteFactorDTO GrupoComponente);
        AsignacionComponenteEvaluacionDTO ActualizarAsignacionComponenteAEvaluacion(AsignacionComponenteEvaluacionDTO asignacionComponenteEvaluacionDTO, string usuario);
        bool ActualizarGrupoComponente(GrupoComponenteEvaluacionFormularioDTO Formulario);
        bool RegistrarGrupoComponente(GrupoComponenteEvaluacionFormularioDTO Formulario, string usuario);
        bool ActualizarCentilGrupoComponente(ObjetoCentilCompuestoDTO CentilFormulario);
    }
}
