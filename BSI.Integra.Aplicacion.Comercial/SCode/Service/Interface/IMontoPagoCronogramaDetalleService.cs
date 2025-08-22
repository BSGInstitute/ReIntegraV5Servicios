using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IMontoPagoCronogramaDetalleService
    {
        #region Metodos Base
        MontoPagoCronogramaDetalle Add(MontoPagoCronogramaDetalle entidad);
        MontoPagoCronogramaDetalle Update(MontoPagoCronogramaDetalle entidad);
        bool Delete(int id, string usuario);

        List<MontoPagoCronogramaDetalle> Add(List<MontoPagoCronogramaDetalle> listadoEntidad);
        List<MontoPagoCronogramaDetalle> Update(List<MontoPagoCronogramaDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetalle();
        IEnumerable<MontoPagoCronogramaDetalleComboDTO> ObtenerCombo();
        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetallePorIdCronograma(int idCronograma);
        IEnumerable<MontoPagoCronogramaDetalle> MapeoEntidadesDesdeListaDTO(List<MontoPagoCronogramaDetalleDTO> listaDto);
        IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerPorIdCronograma(int idCronograma);
    }
}
