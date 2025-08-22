using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorCalificacionRepository : IGenericRepository<TProveedorCalificacion>
    {
        #region Metodos Base
        TProveedorCalificacion Add(ProveedorCalificacion entidad);
        TProveedorCalificacion Update(ProveedorCalificacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProveedorCalificacion> Add(IEnumerable<ProveedorCalificacion> listadoEntidad);
        IEnumerable<TProveedorCalificacion> Update(IEnumerable<ProveedorCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProveedorCalificacionDTO> ObtenerProveedorCalificacion();
    }
}
