using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ITipoMovimientoCajaService
    {
        #region Metodos Base
        TipoMovimientoCaja Add(TipoMovimientoCaja entidad);
        TipoMovimientoCaja Update(TipoMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        List<TipoMovimientoCaja> Add(List<TipoMovimientoCaja> listadoEntidad);
        List<TipoMovimientoCaja> Update(List<TipoMovimientoCaja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public List<TipoMovimientoCajaDTO> ObtenerListaTipoMovimientoCaja();
    }
}
