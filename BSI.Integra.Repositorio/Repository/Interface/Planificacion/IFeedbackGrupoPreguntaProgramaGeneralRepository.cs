using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackGrupoPreguntaProgramaGeneralRepository : IGenericRepository<TFeedbackGrupoPreguntaProgramaGeneral>
    {
        #region Metodos Base
        TFeedbackGrupoPreguntaProgramaGeneral Add(FeedbackGrupoPreguntaProgramaGeneral entidad);
        TFeedbackGrupoPreguntaProgramaGeneral Update(FeedbackGrupoPreguntaProgramaGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> Add(IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> listadoEntidad);
        IEnumerable<TFeedbackGrupoPreguntaProgramaGeneral> Update(IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<FeedbackGrupoPreguntaProgramaGeneral> ObtenerPorIdFeedbackConfigurar(int id);


    }
}