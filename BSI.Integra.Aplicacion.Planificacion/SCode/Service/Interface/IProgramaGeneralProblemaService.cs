using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralProblemaService
    {
        #region Metodos Base
        ProgramaGeneralProblema Add(ProgramaGeneralProblema entidad);
        ProgramaGeneralProblema Update(ProgramaGeneralProblema entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralProblema> Add(List<ProgramaGeneralProblema> listadoEntidad);
        List<ProgramaGeneralProblema> Update(List<ProgramaGeneralProblema> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaDTO> ObtenerProgramaGeneralProblema();
        IEnumerable<ProgramaGeneralProblemaComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralProblemaAgendaDTO> ObtenerProgramaGeneralProblemaParaAgendaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObtenerProgramaGeneralProblemaDetalleParaAgendaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDTO> ObtenerProblemaArgumentoModalidad();
        IEnumerable<ProgramaGeneralProblemaArgumentoModalidadDetalleDTO> ObtenerProblemaArgumentoModalidadDetalle();
    }
}
