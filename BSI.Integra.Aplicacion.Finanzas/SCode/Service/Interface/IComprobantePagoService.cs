using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IComprobantePagoService
    {
        #region Metodos Base
        ComprobantePago Add(ComprobantePago entidad);
        ComprobantePago Update(ComprobantePago entidad);
        bool Delete(int id, string usuario);

        List<ComprobantePago> Add(List<ComprobantePago> listadoEntidad);
        List<ComprobantePago> Update(List<ComprobantePago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComprobantePagoDTO> ObtenerComprobanteAutocomplete(string RucComprobanteParcial);
        ComprobantePago InsertarComprobante(ComprobantePagoInsercionDTO RequestDTO);
        IEnumerable<SunatDocumentoDTO> ObtenerElementosSunatDocumento();
        IEnumerable<ComprobantesNoAsociadosDTO> ObtenerComprobantesNoAsociados();
        IEnumerable<ComprobantePagoAsociadoDTO> ObtenerComprobantePagoPorFur(int idFur);
        ComprobantePago ActualizarComprobante(ComprobantePagoInsercionDTO Comprobante);
        IEnumerable<ComprobantePorFurDTO> ObtenerComprobantesPorFurParaPago(int IdFur);
        List<RucSerieNumeroComprobanteDTO> ObtenerComprobantePorRuc(string RucParcial);
        List<ComprobanteMontoUtilizadoDTO> ObtenerMontoUtilizadoComprobante(int IdComprobante);
    }
}
