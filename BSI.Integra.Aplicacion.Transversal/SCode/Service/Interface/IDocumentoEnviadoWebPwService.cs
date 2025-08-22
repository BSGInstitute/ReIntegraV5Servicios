using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDocumentoEnviadoWebPwService
    {
        #region Metodos Base
        DocumentoEnviadoWebPw Add(DocumentoEnviadoWebPw entidad);
        DocumentoEnviadoWebPw Update(DocumentoEnviadoWebPw entidad);
        bool Delete(int id, string usuario);

        List<DocumentoEnviadoWebPw> Add(List<DocumentoEnviadoWebPw> listadoEntidad);
        List<DocumentoEnviadoWebPw> Update(List<DocumentoEnviadoWebPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoEnviadoWebPwDTO> ObtenerDocumentoEnviadoWebPw();
    }
}
