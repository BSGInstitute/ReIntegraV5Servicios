using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoEnviadoWebPwRepository : IGenericRepository<TDocumentoEnviadoWebPw>
    {
        #region Metodos Base
        TDocumentoEnviadoWebPw Add(DocumentoEnviadoWebPw entidad);
        TDocumentoEnviadoWebPw AddAsync(DocumentoEnviadoWebPw entidad);
        TDocumentoEnviadoWebPw Update(DocumentoEnviadoWebPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoEnviadoWebPw> Add(IEnumerable<DocumentoEnviadoWebPw> listadoEntidad);
        IEnumerable<TDocumentoEnviadoWebPw> Update(IEnumerable<DocumentoEnviadoWebPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoEnviadoWebPwDTO> ObtenerDocumentoEnviadoWebPw();
    }
}