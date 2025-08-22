using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IEstadoEtapaProcesoSeleccionRepository : IGenericRepository<TEstadoEtapaProcesoSeleccion>
    {
        #region Metodos Base
        TEstadoEtapaProcesoSeleccion Add(EstadoEtapaProcesoSeleccion entidad);
        TEstadoEtapaProcesoSeleccion Update(EstadoEtapaProcesoSeleccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoEtapaProcesoSeleccion> Add(IEnumerable<EstadoEtapaProcesoSeleccion> listadoEntidad);
        IEnumerable<TEstadoEtapaProcesoSeleccion> Update(IEnumerable<EstadoEtapaProcesoSeleccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstadoEtapaProcesoSeleccionDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        EstadoEtapaProcesoSeleccion? ObtenerPorId(int id);
    }
}
