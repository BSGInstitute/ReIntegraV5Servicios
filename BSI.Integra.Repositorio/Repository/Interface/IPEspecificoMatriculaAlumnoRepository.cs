using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoMatriculaAlumnoRepository : IGenericRepository<TPespecificoMatriculaAlumno>
    {
        TPespecificoMatriculaAlumno Add(PEspecificoMatriculaAlumno objetoBO);
        List<PEspecificoMatriculaAlumnoAgendaDTO> ObtenerTodoFiltroAutoComplete(int idMatriculaCabecera);
        List<PespecificoPadrePespecificoHijoDTO> ListaPespecificoPadrePespecificoHijo(int idPEspecifico);
        int? IdUsuarioMoodle(int idAlumno);
        int? IdCursoMoodle(int idEspecifico);
        bool ExisteNuevaAulaVirtual(int idPEspecifico);
        List<PEspecificoCriterioDetalleEntregableDelAlumno> ObtenerCriterioDetalleEntregablesAlumno(int idCriterioEvaluacion, int IdPEspecifico, int IdMatriculaCabecera);
        List<PEspecificoMatriculaAlumnoAgendaDTO> InsertarPEspecificoMatriculaAlumnoRepositorio(PEspecificoMatriculaAlumnoDTO pEspecificoMatriculaAlumnoDTO);
        void ActualizacionTipoMatriculaPEspecifico(int IdPEspecifico, int IdMatriculaCabecera);
        List<DatosCursoMatriculaDTO> ObtenerDatosCursosPorMatricula(int idMatriculaCabecera);
    }
}
