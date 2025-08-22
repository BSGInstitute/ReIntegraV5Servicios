using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IProcesoSeleccionEtapaRepository : IGenericRepository<TProcesoSeleccionEtapa>
    {
        #region Metodos Base
        TProcesoSeleccionEtapa Add(ProcesoSeleccionEtapa entidad);
        TProcesoSeleccionEtapa Update(ProcesoSeleccionEtapa entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProcesoSeleccionEtapa> Add(IEnumerable<ProcesoSeleccionEtapa> listadoEntidad);
        IEnumerable<TProcesoSeleccionEtapa> Update(IEnumerable<ProcesoSeleccionEtapa> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProcesoSeleccionEtapaDTO> ObtenerCombo();
        Task<IEnumerable<ProcesoSeleccionEtapaDTO>> ObtenerComboAsync();
        List<ProcesoSeleccionEtapaDTO> ObtenerEtapaPorIdProcesoSeleccion(int idProcesoSeleccion);
        IEnumerable<ProcesosSeleccionEtapaComboDTO> ObtenerComboProcesoSeleccionEtapa();
        ProcesoSeleccionEtapa ObtenerEtapaPorId(int id);
    }
}
