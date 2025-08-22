using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ILocacionRepository : IGenericRepository<TLocacion>
    {
        #region Metodos Base
        TLocacion Add(Locacion entidad);
        TLocacion Update(Locacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TLocacion> Add(IEnumerable<Locacion> listadoEntidad);
        IEnumerable<TLocacion> Update(IEnumerable<Locacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<Locacion> ObtenerTodo();
        Locacion ObtenerPorId(int id);
        List<Locacion> ObtenerPorIds(List<int> ids);
        IEnumerable<LocacionComboDTO> ObtenerLocacionParaFiltro();
        Task<IEnumerable<LocacionComboDTO>> ObtenerLocacionParaFiltroAsync();
    }
}
