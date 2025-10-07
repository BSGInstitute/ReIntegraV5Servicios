using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAgendaInformacionActividadService
    {
        (OportunidadCompuestoDTO? Oportunidad, PEspecificoPorIdCentroCostoDTO? PEspecifico) ObtenerOportunidadYPEspecificoPorIdActividadDetalle(int idActividadDetalle);
        bool EnvioCorreoAlumno(int idPlantilla, int idPersonal, string emailPersonal, string emailAlumno, int idoportunidad);
        List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula);
        bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data);
        bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta);
        Task<object> ObtenerResumenPrograma(int idPGeneral, int idCentroCosto);
        Task<ObjetivosResponseDTO> GetObjetivosAsync(int idPGeneral);
        void InvalidarCache(int idPGeneral);
        void LimpiarTodoCache();
        Task<BeneficioProgramaResponseDTO> GetBeneficiosProgramaAsync(int idPGeneral);
        Task<CertificacionProgramaResponseDTO> GetCertificacionesProgramaAsync(int idPGeneral);
        Task<MetodologiaProgramaResponseDTO> GetMetodologiaProgramaAsync(int idPGeneral);
        Task<PautasComplementariasProgramaResponseDTO> GetPautasComplementariasProgramaAsync(int idPGeneral);
        Task<PerfilProfesionalClienteResponseDTO> GetPerfilProfesionalClienteAsync(int idAlumno);
        Task<object> ObtenerSilaboPorIdAsync(int idPgeneral);
    }
}
