using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICategoriaCiudadRepository : IGenericRepository<TCategoriaCiudad>
    {
        #region Metodos Base
        TCategoriaCiudad Add(CategoriaCiudad entidad);
        TCategoriaCiudad Update(CategoriaCiudad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCategoriaCiudad> Add(IEnumerable<CategoriaCiudad> listadoEntidad);
        IEnumerable<TCategoriaCiudad> Update(IEnumerable<CategoriaCiudad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CategoriaCiudad ObtenerPorId(int id);
        IEnumerable<CategoriaCiudad> ObtenerPorIds(List<int> id);
        string? ObtenerTroncalPorIdCiudadIdCategoria(int idCiudad, int idCategoriaPrograma);
        IEnumerable<TroncalDTO> ObtenerTroncales();
        TroncalEntidadDTO ValidarPorCiudadCategoria(int idCategoriaPrograma, int idRegionCiudad);
        TroncalEntidadDTO ValidarTroncal(string troncalCompleto);
    }
}
