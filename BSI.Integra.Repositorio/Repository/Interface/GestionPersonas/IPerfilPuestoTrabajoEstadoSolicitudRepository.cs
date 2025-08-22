using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPerfilPuestoTrabajoEstadoSolicitudRepository : IGenericRepository<TPerfilPuestoTrabajoEstadoSolicitud>
    {
        #region Metodos Base
        TPerfilPuestoTrabajoEstadoSolicitud Add(PerfilPuestoTrabajoEstadoSolicitud entidad);
        TPerfilPuestoTrabajoEstadoSolicitud Update(PerfilPuestoTrabajoEstadoSolicitud entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> Add(IEnumerable<PerfilPuestoTrabajoEstadoSolicitud> listadoEntidad);
        IEnumerable<TPerfilPuestoTrabajoEstadoSolicitud> Update(IEnumerable<PerfilPuestoTrabajoEstadoSolicitud> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PerfilPuestoTrabajoEstadoSolicitudDTO> Obtener();
        PerfilPuestoTrabajoEstadoSolicitud? ObtenerPorId(int id);
    }
}
