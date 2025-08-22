using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorCriterioCalificacionRepository : IGenericRepository<TProveedorCriterioCalificacion>
    {
        #region Metodos Base
        TProveedorCriterioCalificacion Add(ProveedorCriterioCalificacion entidad);
        TProveedorCriterioCalificacion Update(ProveedorCriterioCalificacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorCriterioCalificacion> Add(IEnumerable<ProveedorCriterioCalificacion> listadoEntidad);
        IEnumerable<TProveedorCriterioCalificacion> Update(IEnumerable<ProveedorCriterioCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
