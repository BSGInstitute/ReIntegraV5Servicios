using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralMotivacionArgumentoService
    {
        #region Metodos Base
        ProgramaGeneralMotivacionArgumento Add(ProgramaGeneralMotivacionArgumento entidad);
        ProgramaGeneralMotivacionArgumento Update(ProgramaGeneralMotivacionArgumento entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralMotivacionArgumento> Add(List<ProgramaGeneralMotivacionArgumento> listadoEntidad);
        List<ProgramaGeneralMotivacionArgumento> Update(List<ProgramaGeneralMotivacionArgumento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralMotivacionArgumentoDTO> ObtenerProgramaGeneralMotivacionArgumento();
        IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralMotivacionArgumentoComboDTO> ObtenerProgramaGeneralMotivacionArgumentoAgendaPorIdMotivacion(int idMotivacion);
    }
}
