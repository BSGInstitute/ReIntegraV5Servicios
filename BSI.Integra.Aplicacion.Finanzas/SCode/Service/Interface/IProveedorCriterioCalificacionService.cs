using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProveedorCriterioCalificacionService
    {
        #region Metodos Base
        ProveedorCriterioCalificacion Add(ProveedorCriterioCalificacion entidad);
        ProveedorCriterioCalificacion Update(ProveedorCriterioCalificacion entidad);
        bool Delete(int id, string usuario);

        List<ProveedorCriterioCalificacion> Add(List<ProveedorCriterioCalificacion> listadoEntidad);
        List<ProveedorCriterioCalificacion> Update(List<ProveedorCriterioCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
