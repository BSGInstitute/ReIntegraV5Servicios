using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralProblemaDetalleSolucionService
    {
        #region Metodos Base
        ProgramaGeneralProblemaDetalleSolucion Add(ProgramaGeneralProblemaDetalleSolucion entidad);
        ProgramaGeneralProblemaDetalleSolucion Update(ProgramaGeneralProblemaDetalleSolucion entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralProblemaDetalleSolucion> Add(List<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad);
        List<ProgramaGeneralProblemaDetalleSolucion> Update(List<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProgramaGeneralProblemaDetalleSolucion();
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(int idProblema, int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(int idProblema, int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProblemaDetalleSolucionPorIdProblema(int idProblema);
        bool ExistePoblemaPorId(int idDetalleSolucion);
    }
}
