using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialEstadoRecepcionRepository : IGenericRepository<TMaterialEstadoRecepcion>
    {
        #region Metodos Base
        TMaterialEstadoRecepcion Add(MaterialEstadoRecepcion entidad);
        TMaterialEstadoRecepcion Update(MaterialEstadoRecepcion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialEstadoRecepcion> Add(IEnumerable<MaterialEstadoRecepcion> listadoEntidad);
        IEnumerable<TMaterialEstadoRecepcion> Update(IEnumerable<MaterialEstadoRecepcion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MaterialEstadoRecepcion? ObtenerPorId(int id);
    }
}
