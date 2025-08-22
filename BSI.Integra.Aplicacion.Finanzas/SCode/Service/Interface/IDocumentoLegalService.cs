using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IDocumentoLegalService
    {
        #region Metodos Base
        DocumentoLegal Add(DocumentoLegal entidad);
        DocumentoLegal Update(DocumentoLegal entidad);
        bool Delete(int id, string usuario);

        List<DocumentoLegal> Add(List<DocumentoLegal> listadoEntidad);
        List<DocumentoLegal> Update(List<DocumentoLegal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoLegalDTO> ObtenerDocumentoLegal();
        IEnumerable<DocumentoLegalComboDTO> ObtenerCombo();
        IEnumerable<DocumentoLegalV2DTO> ObtenerDocumentosLegalesV2();
        IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegalAgenda(int area, string rol, int idpais);
        int InsertarDocumentoLegal(DocumentoLegalV3DTO DocumentoLegal, string Usuario);
        bool ActualizarDocumentoLegal(DocumentoLegalV3DTO DocumentoLegal,string Usuario);

        bool EliminarDocumentoLegal(int IdDocumentoLegal, string Usuario);
    }
}
