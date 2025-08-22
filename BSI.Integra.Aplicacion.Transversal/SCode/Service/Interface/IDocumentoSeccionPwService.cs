using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDocumentoSeccionPwService
    {
        #region Metodos Base
        DocumentoSeccionPw Add(DocumentoSeccionPw entidad);
        DocumentoSeccionPw Update(DocumentoSeccionPw entidad);
        bool Delete(int id, string usuario);

        List<DocumentoSeccionPw> Add(List<DocumentoSeccionPw> listadoEntidad);
        List<DocumentoSeccionPw> Update(List<DocumentoSeccionPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<DocumentoSeccionPwDTO> ObtenerDocumentoSeccionPw();
        IEnumerable<DocumentoSeccionPwComboDTO> ObtenerCombo();
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2(int idPGeneral);
        List<SeccionDocumentoDTO> ObtenerSecciones(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1(int idPGeneral);
        List<ProgramaExpositoresDTO> ObtenerExpositoresPorIdGeneral(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumento(int idPgeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricular(int idPGeneral);
        List<EstructuraCursoDTO> ObtenerEstructuraCurso(int idPGeneral);
        List<SeccionDocumentoDTO> ObtenerDocumentoSeccionCompleto(int idPGeneral);
        List<DocumentoSeccionPwFiltroAgrupadoDTO> ObtenerDocumentoSeccionEditar(int idDocumento);
        void EliminacionDocumentoSeccionLogicoPorIdDocumento(int idDocumento, string usuario, List<DocumentoSeccionPwFiltroDTO> nuevos);
    }
}
