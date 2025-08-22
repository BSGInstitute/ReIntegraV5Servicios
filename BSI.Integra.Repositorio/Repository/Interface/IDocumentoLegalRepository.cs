using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoLegalRepository : IGenericRepository<TDocumentoLegal>
    {
        #region Metodos Base
        TDocumentoLegal Add(DocumentoLegal entidad);
        TDocumentoLegal Update(DocumentoLegal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoLegal> Add(IEnumerable<DocumentoLegal> listadoEntidad);
        IEnumerable<TDocumentoLegal> Update(IEnumerable<DocumentoLegal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoLegalDTO> ObtenerDocumentoLegal();
        IEnumerable<DocumentoLegalComboDTO> ObtenerCombo();
        IEnumerable<DocumentoLegalAgendaDTO> ObtenerDocumentoLegalParaAgenda(int idAreaPersonal, string rol, int idPais);
        IEnumerable<DocumentoLegalV2DTO> ObtenerDocumentosLegales();
        IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegalAgenda(int area, string rol, int idpais);
    }
}