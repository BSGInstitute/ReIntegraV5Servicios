using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralProblemaModalidadService
    {
        #region Metodos Base
        ProgramaGeneralProblemaModalidad Add(ProgramaGeneralProblemaModalidad entidad);
        ProgramaGeneralProblemaModalidad Update(ProgramaGeneralProblemaModalidad entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralProblemaModalidad> Add(List<ProgramaGeneralProblemaModalidad> listadoEntidad);
        List<ProgramaGeneralProblemaModalidad> Update(List<ProgramaGeneralProblemaModalidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerProgramaGeneralProblemaModalidad();
        IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerModalidadPorIdProblema(int idProblema);
        public void EliminacionLogicaPorProblema(int idProblema, string usuario, List<ModalidadCursoProblemaDTO> nuevos);
        ProgramaGeneralProblemaModalidad ObtenerEntidadPorId(int idProgramaGeneralProblemaModalidad);
    }
}
