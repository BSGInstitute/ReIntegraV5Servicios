using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaRepository : IGenericRepository<TProgramaGeneralProblema>
    {
        #region Metodos Base
        TProgramaGeneralProblema Add(ProgramaGeneralProblema entidad);
        TProgramaGeneralProblema Update(ProgramaGeneralProblema entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblema> Add(IEnumerable<ProgramaGeneralProblema> listadoEntidad);
        IEnumerable<TProgramaGeneralProblema> Update(IEnumerable<ProgramaGeneralProblema> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaDTO> ObtenerProgramaGeneralProblema();
        ProgramaGeneralProblema ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralProblemaComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidadNuevaAgenda(int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDTO> ObtenerProblemaArgumentoModalidad();
        List<CompuestoProblemaModalidadAlternoDTO> ObteneProblemasPorModalidades(int idPGeneral);

    }
}
