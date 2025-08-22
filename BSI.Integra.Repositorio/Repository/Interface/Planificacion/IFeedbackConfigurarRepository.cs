using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackConfigurarRepository : IGenericRepository<TFeedbackConfigurar>
    {
        #region Metodos Base
        TFeedbackConfigurar Add(FeedbackConfigurar entidad);
        TFeedbackConfigurar Update(FeedbackConfigurar entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFeedbackConfigurar> Add(IEnumerable<FeedbackConfigurar> listadoEntidad);
        IEnumerable<TFeedbackConfigurar> Update(IEnumerable<FeedbackConfigurar> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerTodoFeedbackConfigurarFiltro();
        IEnumerable<FeedbackConfigurarFiltroDTO> Obtener();
        
        FeedbackConfigurar ObtenerPorId(int id);
    }

}