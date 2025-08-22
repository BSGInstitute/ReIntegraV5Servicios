using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaModalidadRepository : IGenericRepository<TProgramaGeneralProblemaModalidad>
    {
        #region Metodos Base
        TProgramaGeneralProblemaModalidad Add(ProgramaGeneralProblemaModalidad entidad);
        TProgramaGeneralProblemaModalidad Update(ProgramaGeneralProblemaModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaModalidad> Add(IEnumerable<ProgramaGeneralProblemaModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaModalidad> Update(IEnumerable<ProgramaGeneralProblemaModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerProgramaGeneralProblemaModalidad();
        IEnumerable<ProgramaGeneralProblemaModalidadDTO> ObtenerModalidadPorIdProblema(int idProblema);
    }
}