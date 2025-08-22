using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGastoFinancieroCronogramaRepository : IGenericRepository<TGastoFinancieroCronograma>
    {
        #region Metodos Base
        TGastoFinancieroCronograma Add(GastoFinancieroCronograma entidad);
        TGastoFinancieroCronograma Update(GastoFinancieroCronograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGastoFinancieroCronograma> Add(IEnumerable<GastoFinancieroCronograma> listadoEntidad);
        IEnumerable<TGastoFinancieroCronograma> Update(IEnumerable<GastoFinancieroCronograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<GastoFinancieroCronogramaDatosDTO> ObtenerGastoFinancieroCronograma();
         List<ReporteDePrestamoDTO> ObtenerReportePrestamos(int IdEntidadFinanciera, int IdPrestamo);
         List<FiltroDTO> ObtenerListaEntidadFinancieraPrestamo();
        List<PrestamoFiltroDTO> ObtenerListaPrestamosFiltro();


    }
}
