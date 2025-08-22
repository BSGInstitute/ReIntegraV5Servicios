using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAreaTrabajoRepository : IGenericRepository<TAreaTrabajo>
    {
        #region Metodos Base
        TAreaTrabajo Add(AreaTrabajo entidad);
        TAreaTrabajo Update(AreaTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAreaTrabajo> Add(IEnumerable<AreaTrabajo> listadoEntidad);
        IEnumerable<TAreaTrabajo> Update(IEnumerable<AreaTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        AreaTrabajo ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<ComboDTO> ObtenerAreaAgenda();
        IEnumerable<ComboDTO> ObtenerTodoAreaTrabajoFiltro();
    }
}
