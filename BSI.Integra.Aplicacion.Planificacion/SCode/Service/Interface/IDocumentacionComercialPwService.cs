using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IDocumentacionComercialPwService
    {
        #region Metodos Base
        DocumentacionComercialPw Add(DocumentacionComercialPw entidad);
        DocumentacionComercialPw Update(DocumentacionComercialPw entidad);
        bool Delete(int id, string usuario);

        List<DocumentacionComercialPw> Add(List<DocumentacionComercialPw> listadoEntidad);
        List<DocumentacionComercialPw> Update(List<DocumentacionComercialPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentacionComercialPwDTO> ObtenerDocumentacionComercialPw();
        IEnumerable<DocumentacionComercialPwComboDTO> ObtenerCombo();
        StringDTO ObtenerContenidoDocumentoComercial(string tipoDocumento, string modalidad, int idPais);
        DocumentoComercialContenidoDTO DocumentoComercialContenido(string tipoDocumento, string modalidad, int idPais);
    }
}
