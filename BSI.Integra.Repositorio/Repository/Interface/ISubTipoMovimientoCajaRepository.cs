using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISubTipoMovimientoCajaRepository : IGenericRepository<TSubTipoMovimientoCaja>
    {
        #region Metodos Base
        TSubTipoMovimientoCaja Add(SubTipoMovimientoCaja entidad);
        TSubTipoMovimientoCaja Update(SubTipoMovimientoCaja entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSubTipoMovimientoCaja> Add(IEnumerable<SubTipoMovimientoCaja> listadoEntidad);
        IEnumerable<TSubTipoMovimientoCaja> Update(IEnumerable<SubTipoMovimientoCaja> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<SubTipoMovimientoCajaDTO> ObtenerListaSubTipoMovimientoCaja();
    }
}
