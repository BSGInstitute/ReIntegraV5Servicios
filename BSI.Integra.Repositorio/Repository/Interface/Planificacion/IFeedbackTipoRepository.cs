using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFeedbackTipoRepository: IGenericRepository<TFeedbackTipo>
    {
        #region Metodos Base
        TFeedbackTipo Add(FeedbackTipo entidad);
        TFeedbackTipo Update(FeedbackTipo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFeedbackTipo> Add(IEnumerable<FeedbackTipo> listadoEntidad);
        IEnumerable<TFeedbackTipo> Update(IEnumerable<FeedbackTipo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FeedbackTipoDTO> Obtener();
        FeedbackTipo ObtenerPorId(int id);

    }
}
