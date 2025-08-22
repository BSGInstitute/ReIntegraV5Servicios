using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IProveedorCalificacionService
    {
        #region Metodos Base
        ProveedorCalificacion Add(ProveedorCalificacion entidad);
        ProveedorCalificacion Update(ProveedorCalificacion entidad);
        bool Delete(int id, string usuario);

        List<ProveedorCalificacion> Add(List<ProveedorCalificacion> listadoEntidad);
        List<ProveedorCalificacion> Update(List<ProveedorCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProveedorCalificacionDTO> ObtenerProveedorCalificacion();

    }
}
