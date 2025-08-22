using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoLegalPaisRepository : IGenericRepository<TDocumentoLegalPai>
    {
        #region Metodos Base
        TDocumentoLegalPai Add(DocumentoLegalPais entidad);
        TDocumentoLegalPai Update(DocumentoLegalPais entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoLegalPai> Add(IEnumerable<DocumentoLegalPais> listadoEntidad);
        IEnumerable<TDocumentoLegalPai> Update(IEnumerable<DocumentoLegalPais> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoLegalPaisDTO> ObtenerDocumentoLegalPais();
        IEnumerable<DocumentoLegalPaisComboDTO> ObtenerCombo();
    }
}