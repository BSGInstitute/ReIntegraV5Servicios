using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IFeedbackConfigurarGrupoPreguntaService
    {
        FeedbackComboDTO ObtenerCombos();
        (List<int> ProgramasGenerales, List<int> ProgramasEspecificos) ObtenerListaProgramasSelecionados(int idConfiguracion);
        FeedbackConfigurarGrupoPreguntaDTO Insertar(RegistroFeedbackConfigurarGrupoPreguntaDTO dto, string usuario);
        FeedbackConfigurarGrupoPreguntaDTO Actualizar(RegistroFeedbackConfigurarGrupoPreguntaDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
