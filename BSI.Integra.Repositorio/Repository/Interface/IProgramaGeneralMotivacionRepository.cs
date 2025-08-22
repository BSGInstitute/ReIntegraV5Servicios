using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralMotivacionRepository : IGenericRepository<TProgramaGeneralMotivacion>
    {
        #region Metodos Base
        TProgramaGeneralMotivacion Add(ProgramaGeneralMotivacion entidad);
        TProgramaGeneralMotivacion Update(ProgramaGeneralMotivacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralMotivacion> Add(IEnumerable<ProgramaGeneralMotivacion> listadoEntidad);
        IEnumerable<TProgramaGeneralMotivacion> Update(IEnumerable<ProgramaGeneralMotivacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralMotivacionDTO> ObtenerProgramaGeneralMotivacion();
        IEnumerable<ProgramaGeneralMotivacionComboDTO> ObtenerCombo();
        IEnumerable<ProgramaGeneralMotivacionAgendaDTO> ObtenerMotivacionesParaAgendaPorIdOportunidad(int idOportunidad);
        ProgramaGeneralMotivacion? ObtenerPorId(int id);
        List<CompuestoMotivacionModalidadAlternoDTO> ObteneMotivacionesPorModalidades(int idPGeneral);
    }
}