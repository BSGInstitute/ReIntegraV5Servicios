using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoSeccionPwRepository : IGenericRepository<TDocumentoSeccionPw>
    {
        #region Metodos Base
        TDocumentoSeccionPw Add(DocumentoSeccionPw entidad);
        TDocumentoSeccionPw Update(DocumentoSeccionPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoSeccionPw> Add(IEnumerable<DocumentoSeccionPw> listadoEntidad);
        IEnumerable<TDocumentoSeccionPw> Update(IEnumerable<DocumentoSeccionPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoSeccionPwDTO> ObtenerDocumentoSeccionPw();
        IEnumerable<DocumentoSeccionPwComboDTO> ObtenerCombo();
        List<SeccionDocumentoDTO> ObtenerDocumentoSeccion(int idPgeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumento(int idPgeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoSpeech(int idPgeneral);
        Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoAsync(int idPgeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricular(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricularPorIdsPGeneral(int idPGeneral,int IdHijo);
        Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoEstructuraCurricularAsync(int idPGeneral);
        Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerSeccionDocumentoEstructuraCurriculaPorIdsPGeneralAsync(int idProgramaGeneral,List<int> idPGeneral);
        List<SeccionDocumentoDTO> ObtenerSecciones(int idPGeneral);
        Task<List<SeccionDocumentoDTO>> ObtenerSeccionesAsync(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2Objetivos(int idPGeneral);
        Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerDatosComplementariosProgramaGeneralV2Async(int idPGeneral);
        Task<List<RegistroListaSeccionesDocumentoDTO>> ObtenerDatosComplementariosProgramaGeneralV1Async(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1(int idPGeneral);
        List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1Speech(int idPGeneral);
        List<ProgramaExpositoresDTO> ObtenerExpositoresPorIdGeneral(int idPGeneral);
        List<EstructuraCursoDTO> ObtenerEstructuraCurso(int IdMatriculaCabecera);
        List<EstructuraCursoDTO> ObtenerEstructuraCursoPorIdProgramaGeneral(int IdProgramaGeneral);

        List<SeccionDocumentoDTO> ObtenerDocumentoSeccionCompleto(int idPGeneral);
        Task<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>> ObtenerSesionesPreguntasInteractivasExportacionExcel();
        Task<IEnumerable<SesionSubSesionPreguntaInteractivaDTO>> ObtenerSubSesionesPreguntasInteractivasExportacionExcel();
        IEnumerable<EstructuraProgramaCapituloDTO> ObtenerCapituloPrograma();
        IEnumerable<SesionSubSesionPreguntaInteractivaDTO> ObtenerEstructuraProgramaSesion();
        List<DocumentoSeccionPwFiltroDTO> ObtenerDocumentoSeccionPorId(int idDocumentoSeccion);
        DocumentoSeccionPw? ObtenerIdSeccionIdDocumentoContenido(int idSeccionPw, int idDocumentoPw, string contenido);
        DocumentoSeccionPw? ObtenerPorIdSeccionIdDocumento(int idSeccionPw, int idDocumentoPw);
        List<DocumentoSeccionPw> ObtenerPorIdDocumento(int idDocumentoPw);
        Task<List<CursoHijoV2DTO>> ObtenerCursosHijosPorIdPGeneralAsync(int idPGeneral);
        Task<List<RegistroSeccionContenidoDTO>> ObtenerEstructuraCurricularPorIdHijoAsync(int idHijo);
        Task<string> ObtenerNotaEstructuraCurricularAsync(int idPGeneral);
    }
}