using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDocumentoAgendaRepository : IGenericRepository<TDocumentoAgendum>
    {
        #region Metodos Base
        TDocumentoAgendum Add(DocumentoAgenda entidad);
        TDocumentoAgendum Update(DocumentoAgenda entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDocumentoAgendum> Add(IEnumerable<DocumentoAgenda> listadoEntidad);
        IEnumerable<TDocumentoAgendum> Update(IEnumerable<DocumentoAgenda> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DocumentoAgendaDTO> ObtenerDocumentoAgenda();
        IEnumerable<DocumentoAgendaComboDTO> ObtenerCombo();
        IEnumerable<DocumentoAgendaSinAuditoriaDTO> ObtenerDocumentoAgendaSinAuditoria();
        Task<List<ResumenPrograma2DTO>> ObtenerResumenProgramaPorIdPGeneralAsync(int idPGeneral);
        Task<string> ObtenerPrerrequisitosPorIdPGeneralAsync(int idPGeneral);
        Task<List<EstructuraCurricularDTO>> ObtenerContenidoEstructuraCurricularAsync(int idPGeneral);
        string ObtenerPresentacionPorIdPGeneral(int idPGeneral);
        Task<string> ObtenerPresentacionPorIdPGeneralAsync(int idPGeneral);
        string ObtenerPublicoObjetivoPorIdPGeneral(int idPGeneral);
        Task<string> ObtenerPublicoObjetivoPorIdPGeneralAsync(int idPGeneral);
        string ObtenerDuracionHorariosPorIdPGeneral(int idPGeneral);
        Task<string> ObtenerDuracionHorariosPorIdPGeneralAsync(int idPGeneral);
        string ObtenerPrerrequisitosPorIdPGeneral(int idPGeneral);
        string ObtenerExpositoresPorIdPGeneral(int idPGeneral);
        Task<List<ProgramaExpositoresDTO>> ObtenerExpositoresPorIdPGeneralAsync(int idPGeneral);



        StringDTO ObtenerDocumentoAgendaUrlPorPais(int idDocumentoAgenda, int idPais);
        List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula);
        List<PEspecificoSesionEncuestaPreguntaDTO> ObtenerPreguntasSesionEncuestaIdPespecifico(int IdPEspecificoSesion);
        List<PEspecificoSesionEncuestaAlumnoDTO> ObtenerEncuestaAlumnoPorIdPEspecificoSesion(int IdPEspecificoSesion, int IdMatriculaCabecera);
        List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> ObtenerPEspecificoSesionEncuestaPreguntaAlternativaPorIdSesion(int IdPEspecificoSesion);
        List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> ObtenerPEspecificoSesionEncuestaAlumnoRespuestaPorIdSesion(int IdPEspecificoSesion, int IdMatriculaCabecera);
        bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data);
        bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta);
        Task<ObjetivosRawDTO> GetObjetivosRawAsync(int idPGeneral);
        Task<List<ResumenProgramaV3DTO>> ObtenerResumenProgramaPorIdPGeneral(int idPGeneral, int idCentroCosto);
        Task<List<BeneficioRawDTO>> GetBeneficiosRawAsync(int idPGeneral);
        Task<List<CertificacionRawDTO>> GetCertificacionesRawAsync(int idPGeneral);
        Task<MetodologiaRawDTO> GetMetodologiaRawAsync(int idPGeneral);
        Task<PautasComplementariasRawDTO> GetPautasComplementariasRawAsync(int idPGeneral);
        Task<PerfilProfesionalClienteDTO> ObtenerPerfilProfesionalClienteAsync(int idAlumno);
        Task<int> ObtenerIdPGeneralPorIdCentroCostoAsync(int idCentroCosto);
        List<SeccionDocumentov2DTO> ObtenerDocumentoSeccion(int idPgeneral);
        PgeneralDocumentoSeccionv2DTO ObtenerPgeneralDocumentoPorId(int id);
        List<PgeneralHijov2DTO> ObtenerPGeneralHijos(int idPgeneral);
    }
}