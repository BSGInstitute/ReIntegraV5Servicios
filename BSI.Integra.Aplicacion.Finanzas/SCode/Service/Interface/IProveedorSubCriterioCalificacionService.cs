using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProveedorSubCriterioCalificacionService
    {
        #region Metodos Base
        ProveedorSubCriterioCalificacion Add(ProveedorSubCriterioCalificacion entidad);
        ProveedorSubCriterioCalificacion Update(ProveedorSubCriterioCalificacion entidad);
        bool Delete(int id, string usuario);

        List<ProveedorSubCriterioCalificacion> Add(List<ProveedorSubCriterioCalificacion> listadoEntidad);
        List<ProveedorSubCriterioCalificacion> Update(List<ProveedorSubCriterioCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        object ObtenerSubCriterioCalificacion();
    }
}
