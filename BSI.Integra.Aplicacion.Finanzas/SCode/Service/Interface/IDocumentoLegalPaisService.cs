using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IDocumentoLegalPaisService
    {
        #region Metodos Base
        DocumentoLegalPais Add(DocumentoLegalPais entidad);
        DocumentoLegalPais Update(DocumentoLegalPais entidad);
        bool Delete(int id, string usuario);

        List<DocumentoLegalPais> Add(List<DocumentoLegalPais> listadoEntidad);
        List<DocumentoLegalPais> Update(List<DocumentoLegalPais> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoLegalPaisDTO> ObtenerDocumentoLegalPais();
        IEnumerable<DocumentoLegalPaisComboDTO> ObtenerCombo();
    }
}
