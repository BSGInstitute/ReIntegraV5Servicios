using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGastoFinancieroCronogramaDetalleRepository : IGenericRepository<TGastoFinancieroCronogramaDetalle>
    {
        #region Metodos Base
        TGastoFinancieroCronogramaDetalle Add(GastoFinancieroCronogramaDetalle entidad);
        TGastoFinancieroCronogramaDetalle Update(GastoFinancieroCronogramaDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGastoFinancieroCronogramaDetalle> Add(IEnumerable<GastoFinancieroCronogramaDetalle> listadoEntidad);
        IEnumerable<TGastoFinancieroCronogramaDetalle> Update(IEnumerable<GastoFinancieroCronogramaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GastoFinancieroCronogramaDetalleDTO> ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(int IdCronograma);

    }
}
