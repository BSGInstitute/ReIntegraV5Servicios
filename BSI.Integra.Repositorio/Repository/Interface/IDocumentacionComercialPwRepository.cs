using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentacionComercialPwRepository : IGenericRepository<TDocumentacionComercialPw>
    {
        #region Metodos Base
        TDocumentacionComercialPw Add(DocumentacionComercialPw entidad);
        TDocumentacionComercialPw Update(DocumentacionComercialPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentacionComercialPw> Add(IEnumerable<DocumentacionComercialPw> listadoEntidad);
        IEnumerable<TDocumentacionComercialPw> Update(IEnumerable<DocumentacionComercialPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentacionComercialPwDTO> ObtenerDocumentacionComercialPw();
        IEnumerable<DocumentacionComercialPwComboDTO> ObtenerCombo();
        StringDTO ObtenerContenidoDocumentoComercial(string tipoDocumento, string modalidad, int idPais);
        DocumentoComercialContenidoDTO DocumentoComercialContenido(string tipoDocumento, string modalidad, int idPais);
    }
}