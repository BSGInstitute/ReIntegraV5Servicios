using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IGastoFinancieroCronogramaService
    {
        #region Metodos Base
        GastoFinancieroCronograma Add(CronogramaEnvioDTO data);
        GastoFinancieroCronograma Update(CronogramaEnvioDTO data);
        bool Delete(int id, string usuario);

        List<GastoFinancieroCronograma> Add(List<GastoFinancieroCronograma> listadoEntidad);
        List<GastoFinancieroCronograma> Update(List<GastoFinancieroCronograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool EliminarCrogramayDetalle(int IdCronograma, string usuario);

        bool InsertarCronogramaYDetalle(CronogramaYDetalleDTO Json);
        bool ActualizarCronogramaYDetalle(CronogramaYDetalleDTO Json);
        object GenerarReportePrestamos(FiltroReportePrestamoDTO Filtro);
         object ObtenerListaEntidadesFinancierasConPrestamo();
         Object ObtenerListaPrestamos();
    }
}
