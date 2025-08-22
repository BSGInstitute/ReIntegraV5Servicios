using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaDetalleSolucionRepository : IGenericRepository<TProgramaGeneralProblemaDetalleSolucion>
    {
        #region Metodos Base
        TProgramaGeneralProblemaDetalleSolucion Add(ProgramaGeneralProblemaDetalleSolucion entidad);
        TProgramaGeneralProblemaDetalleSolucion Update(ProgramaGeneralProblemaDetalleSolucion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaDetalleSolucion> Add(IEnumerable<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaDetalleSolucion> Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProgramaGeneralProblemaDetalleSolucion();
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionComboDTO> ObtenerCombo();
        ProgramaGeneralProblemaDetalleSolucion ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgenda(int idProgramaGeneralProblema, int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionAgendaNuevaAgendaDTO> ObtenerProgramaGeneralProblemaDetalleSolucionParaAgendaNuevaAgenda(int idProgramaGeneralProblema, int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionDTO> ObtenerProblemaDetalleSolucionPorIdProblema(int idProblema);
    }
}