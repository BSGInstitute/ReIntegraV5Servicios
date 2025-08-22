using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICajaPorRendirService
    {
        #region Metodos Base
        CajaPorRendir Add(CajaPorRendir entidad);
        CajaPorRendir Update(CajaPorRendir entidad);
        bool Delete(int id, string usuario);

        List<CajaPorRendir> Add(List<CajaPorRendir> listadoEntidad);
        List<CajaPorRendir> Update(List<CajaPorRendir> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CajaPorRendirDTO> ObtenerCajaPorRendir(CajaPorRendirFiltroDTO filtro);
        IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitante(int idPersonalResponsable);
        bool EliminarCajaPorRendirSolicitudEnviada(int id, int idFur, string usuario);
        bool DevolverSolicitudEnviada(CajaPorRendirDevolerDTO data);
        MontoCajaDTO ObtenerMontoTotalCaja(int IdCaja);
        bool GenerarPorRendir(GenerarPorRendirDTO generacionPorRendirDTO);
        bool GenerarPorRendirInmediato(GenerarPorRendirInmediatoDTO PorRendirInmediatoDTO);
        IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerCajaPorRendirByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);
        IEnumerable<byte[]> ObtenerDocumentosCajaPorRendir(List<int> listaEnteros);
        IEnumerable<CajaPorRendirCabeceraRendicionDTO> ObtenerCajasPorRendirParaRendicion(int IdUsuario);
        IEnumerable<CajaPorRendirDTO> ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera);
        List<CajaPorRendirDTO> ObtenerCajasPorRendirFinanzas(int IdUsuario);
        decimal ObtenerMontoLimiteSolicitud(int IdFur);
        bool InsertarCajaPorRendir(DatosSolicitudDTO ObjetoDTO);
        bool ActualizarCajaPorRendir(DatosSolicitudDTO ObjetoDTO);
        bool ActualizarCajaPorRendirPonerEnviado(EsEnviadoSolicitudDTO CajasPorRendir);
    }
}
