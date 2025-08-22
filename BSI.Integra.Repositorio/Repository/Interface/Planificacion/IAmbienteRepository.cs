using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IAmbienteRepository : IGenericRepository<TAmbiente>
    {
        #region Metodos Base
        TAmbiente Add(Ambiente entidad);
        TAmbiente Update(Ambiente entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TAmbiente> Add(IEnumerable<Ambiente> listadoEntidad);
        IEnumerable<TAmbiente> Update(IEnumerable<Ambiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<Ambiente> ObtenerTodo();
        Ambiente? ObtenerPorId(int id);
        List<Ambiente> ObtenerPorIds(List<int> ids);
        IEnumerable<AmbienteCiudadFiltroDTO> ObtenerAmbienteCiudadFiltro();
        Task<IEnumerable<AmbienteCiudadFiltroDTO>> ObtenerAmbienteCiudadFiltroAsync();
    }
}
