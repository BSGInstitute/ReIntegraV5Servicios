using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorSubCriterioCalificacionRepository : IGenericRepository<TProveedorSubCriterioCalificacion>
    {
        #region Metodos Base
        TProveedorSubCriterioCalificacion Add(ProveedorSubCriterioCalificacion entidad);
        TProveedorSubCriterioCalificacion Update(ProveedorSubCriterioCalificacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorSubCriterioCalificacion> Add(IEnumerable<ProveedorSubCriterioCalificacion> listadoEntidad);
        IEnumerable<TProveedorSubCriterioCalificacion> Update(IEnumerable<ProveedorSubCriterioCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        object ObtenerSubCriterioCalificacion();
    }
}
