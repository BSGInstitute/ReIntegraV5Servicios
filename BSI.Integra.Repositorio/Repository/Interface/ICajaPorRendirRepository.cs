using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICajaPorRendirRepository : IGenericRepository<TCajaPorRendir>
    {
        #region Metodos Base
        TCajaPorRendir Add(CajaPorRendir entidad);
        TCajaPorRendir Update(CajaPorRendir entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCajaPorRendir> Add(IEnumerable<CajaPorRendir> listadoEntidad);
        IEnumerable<TCajaPorRendir> Update(IEnumerable<CajaPorRendir> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CajaPorRendirDTO> ObtenerCajaPorRendir(CajaPorRendirFiltroDTO filtro);
        IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitante(int idPersonalResponsable);
        MontoCajaDTO ObtenerMontoTotalCaja(int IdCaja);
        IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerCajaPorRendirByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);
        IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerDatosCajaPorRendir(int[] IdPorRendirCabecera);
        IEnumerable<CajaPorRendirCabeceraRendicionDTO> ObtenerCajasPorRendirParaRendicion(int IdUsuario);
        IEnumerable<CajaPorRendirDTO> ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera);
        List<CajaPorRendirDTO> ObtenerCajasPorRendirFinanzas(int IdUsuario);
    }
}
