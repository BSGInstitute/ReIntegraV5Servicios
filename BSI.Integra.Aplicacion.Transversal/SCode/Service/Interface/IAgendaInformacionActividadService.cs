using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAgendaInformacionActividadService
    {
        (OportunidadCompuestoDTO? Oportunidad, PEspecificoPorIdCentroCostoDTO? PEspecifico) ObtenerOportunidadYPEspecificoPorIdActividadDetalle(int idActividadDetalle);
        bool EnvioCorreoAlumno(int idPlantilla, int idPersonal, string emailPersonal, string emailAlumno, int idoportunidad);
        List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula);
        bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data);
        bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta);

    }
}
