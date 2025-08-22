using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMontoPagoCronogramaDetalleRepository : IGenericRepository<TMontoPagoCronogramaDetalle>
    {
        #region Metodos Base
        TMontoPagoCronogramaDetalle Add(MontoPagoCronogramaDetalle entidad);
        TMontoPagoCronogramaDetalle Update(MontoPagoCronogramaDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPagoCronogramaDetalle> Add(IEnumerable<MontoPagoCronogramaDetalle> listadoEntidad);
        IEnumerable<TMontoPagoCronogramaDetalle> Update(IEnumerable<MontoPagoCronogramaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetalle();
        IEnumerable<MontoPagoCronogramaDetalleComboDTO> ObtenerCombo();
        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetallePorIdCronograma(int idCronograma);
        Task<IEnumerable<MontoPagoCronogramaDetalleDTO>> ObtenerMontoPagoCronogramaDetallePorIdCronogramaAsync(int idCronograma);
        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerPorIdCronograma(int idCronograma);
    }
}