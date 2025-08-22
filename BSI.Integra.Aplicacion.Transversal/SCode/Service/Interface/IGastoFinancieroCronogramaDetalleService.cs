using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IGastoFinancieroCronogramaDetalleService
    {
        #region Metodos Base
        GastoFinancieroCronogramaDetalle Add(CronogramaDetalleEnvioDTO data);
        GastoFinancieroCronogramaDetalle Update(CronogramaDetalleEnvioDTO data);
        bool Delete(int id, string usuario);

        List<GastoFinancieroCronogramaDetalle> Add(List<GastoFinancieroCronogramaDetalle> listadoEntidad);
        List<GastoFinancieroCronogramaDetalle> Update(List<GastoFinancieroCronogramaDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


        IEnumerable<GastoFinancieroCronogramaDetalleDTO> ObtenerListaGastoFinancieroCronogramaDetallePorIdGastoFinanciero(int IdCronograma);

    }
}
