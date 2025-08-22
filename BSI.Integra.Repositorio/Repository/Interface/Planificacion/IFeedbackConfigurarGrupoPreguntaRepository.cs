using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackConfigurarGrupoPreguntaRepository : IGenericRepository<TFeedbackConfigurarGrupoPreguntum>
    {
        #region Metodos Base
        TFeedbackConfigurarGrupoPreguntum Add(FeedbackConfigurarGrupoPregunta entidad);
        TFeedbackConfigurarGrupoPreguntum Update(FeedbackConfigurarGrupoPregunta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFeedbackConfigurarGrupoPreguntum> Add(IEnumerable<FeedbackConfigurarGrupoPregunta> listadoEntidad);
        IEnumerable<TFeedbackConfigurarGrupoPreguntum> Update(IEnumerable<FeedbackConfigurarGrupoPregunta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<FeedbackConfigurarGrupoPreguntaDTO> ObtenerFeedbackConfigurar();
        FeedbackConfigurarGrupoPreguntaDTO? ObtenerFeedbackConfigurarPorId(int id);
        FeedbackConfigurarGrupoPregunta? ObtenerPorId(int id);
    }
}
