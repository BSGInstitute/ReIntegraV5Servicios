using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackGrupoPreguntaProgramaEspecificoRepository : IGenericRepository<TFeedbackGrupoPreguntaProgramaEspecifico>
    {
        #region Metodos Base
        TFeedbackGrupoPreguntaProgramaEspecifico Add(FeedbackGrupoPreguntaProgramaEspecifico entidad);
        TFeedbackGrupoPreguntaProgramaEspecifico Update(FeedbackGrupoPreguntaProgramaEspecifico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> Add(IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> listadoEntidad);
        IEnumerable<TFeedbackGrupoPreguntaProgramaEspecifico> Update(IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FeedbackGrupoPreguntaProgramaEspecifico> ObtenerPorIdFeedbackConfigurar(int id);
    }
}
