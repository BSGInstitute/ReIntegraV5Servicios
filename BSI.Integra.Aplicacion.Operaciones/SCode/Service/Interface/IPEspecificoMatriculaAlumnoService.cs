using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IPEspecificoMatriculaAlumnoService
    {
        List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerTodoFiltroAutoComplete(int idMatriculaCabecera);
        public List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerPEspecificoPorMatricula(int idMatriculaCabecera);
    }
}
