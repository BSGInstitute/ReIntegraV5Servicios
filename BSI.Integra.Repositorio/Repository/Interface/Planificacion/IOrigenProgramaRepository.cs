using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IOrigenProgramaRepository : IGenericRepository<TOrigenPrograma>
    {
        #region Metodos Base
        TOrigenPrograma Add(OrigenPrograma entidad);
        TOrigenPrograma Update(OrigenPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TOrigenPrograma> Add(IEnumerable<OrigenPrograma> listadoEntidad);
        IEnumerable<TOrigenPrograma> Update(IEnumerable<OrigenPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OrigenPrograma> ObtenerTodo();
        OrigenPrograma ObtenerPorId(int id);
        List<OrigenPrograma> ObtenerPorIds(List<int> ids);
        IEnumerable<ComboDTO> ObtenerDatosOrigenPrograma();
        Task<IEnumerable<ComboDTO>> ObtenerDatosOrigenProgramaAsync();
    }
}
