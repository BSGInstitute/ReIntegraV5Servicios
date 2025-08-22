using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralMotivacionService
    {
        #region Metodos Base
        ProgramaGeneralMotivacion Add(ProgramaGeneralMotivacion entidad);
        ProgramaGeneralMotivacion Update(ProgramaGeneralMotivacion entidad);
        bool Delete(int id, string usuario);

        List<ProgramaGeneralMotivacion> Add(List<ProgramaGeneralMotivacion> listadoEntidad);
        List<ProgramaGeneralMotivacion> Update(List<ProgramaGeneralMotivacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralMotivacionDTO> ObtenerProgramaGeneralMotivacion();
        IEnumerable<ProgramaGeneralMotivacionComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> ObtenerMotivacionesDetalleParaAgendaPorIdOportunidad(int idOportunidad);
    }
}
