using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IComprobantePagoRepository : IGenericRepository<TComprobantePago>
    {
        #region Metodos Base
        TComprobantePago Add(ComprobantePago entidad);
        TComprobantePago Update(ComprobantePago entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TComprobantePago> Add(IEnumerable<ComprobantePago> listadoEntidad);
        IEnumerable<TComprobantePago> Update(IEnumerable<ComprobantePago> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComprobantePagoDTO> ObtenerComprobanteAutocomplete(string RucComprobanteParcial);
        IEnumerable<SunatDocumentoDTO> ObtenerElementosSunatDocumento();
        IEnumerable<ComprobantePagoAsociadoDTO> ObtenerComprobantePagoPorFur(int idFur);
        IEnumerable<ComprobantesNoAsociadosDTO> ObtenerComprobantesNoAsociados();
        IEnumerable<ComprobantePorFurDTO> ObtenerComprobantesPorFurParaPago(int IdFur);
        List<RucSerieNumeroComprobanteDTO> ObtenerComprobantePorRuc(string RucParcial);
        List<ComprobanteMontoUtilizadoDTO> ObtenerMontoUtilizadoComprobante(int IdComprobante);
    }
}
