using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralMotivacionArgumentoRepository : IGenericRepository<TProgramaGeneralMotivacionArgumento>
    {
        #region Metodos Base
        TProgramaGeneralMotivacionArgumento Add(ProgramaGeneralMotivacionArgumento entidad);
        TProgramaGeneralMotivacionArgumento Update(ProgramaGeneralMotivacionArgumento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralMotivacionArgumento> Add(IEnumerable<ProgramaGeneralMotivacionArgumento> listadoEntidad);
        IEnumerable<TProgramaGeneralMotivacionArgumento> Update(IEnumerable<ProgramaGeneralMotivacionArgumento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralMotivacionArgumentoDTO> ObtenerProgramaGeneralMotivacionArgumento();
        IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(int idMotivacion);
        ProgramaGeneralMotivacionArgumento? ObtenerPorId(int id);
    }
}