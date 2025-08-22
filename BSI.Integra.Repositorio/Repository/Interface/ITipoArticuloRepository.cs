using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoArticuloRepository : IGenericRepository<TTipoArticulo>
    {
        #region Metodos Base
        TTipoArticulo Add(TipoArticulo entidad);
        TTipoArticulo Update(TipoArticulo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTipoArticulo> Add(IEnumerable<TipoArticulo> listadoEntidad);
        IEnumerable<TTipoArticulo> Update(IEnumerable<TipoArticulo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoArticuloDTO> ObtenerFiltroTipoArticulo();
    }
}
