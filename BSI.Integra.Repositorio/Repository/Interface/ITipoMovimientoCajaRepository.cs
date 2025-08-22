using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoMovimientoCajaRepository : IGenericRepository<TTipoMovimientoCaja>
    {
        #region Metodos Base
        TTipoMovimientoCaja Add(TipoMovimientoCaja entidad);
        TTipoMovimientoCaja Update(TipoMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoMovimientoCaja> Add(IEnumerable<TipoMovimientoCaja> listadoEntidad);
        IEnumerable<TTipoMovimientoCaja> Update(IEnumerable<TipoMovimientoCaja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<TipoMovimientoCajaDTO> ObtenerListaTipoMovimientoCaja();


    }
}
