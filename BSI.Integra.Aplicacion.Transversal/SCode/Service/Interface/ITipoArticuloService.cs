using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoArticuloService
    {
        #region Metodos Base
        TipoArticulo Add(TipoArticulo entidad);
        TipoArticulo Update(TipoArticulo entidad);
        bool Delete(int id, string usuario);
        List<TipoArticulo> Add(List<TipoArticulo> listadoEntidad);
        List<TipoArticulo> Update(List<TipoArticulo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<TipoArticuloDTO> ObtenerFiltroTipoArticulo();
    }
}
