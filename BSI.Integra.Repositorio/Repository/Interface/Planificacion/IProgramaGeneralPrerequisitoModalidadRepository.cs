using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPrerequisitoModalidadRepository : IGenericRepository<TProgramaGeneralPrerequisitoModalidad>
    {
        #region Metodos Base
        TProgramaGeneralPrerequisitoModalidad Add(ProgramaGeneralPrerequisitoModalidad entidad);
        TProgramaGeneralPrerequisitoModalidad Update(ProgramaGeneralPrerequisitoModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPrerequisitoModalidad> Add(IEnumerable<ProgramaGeneralPrerequisitoModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralPrerequisitoModalidad> Update(IEnumerable<ProgramaGeneralPrerequisitoModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPrerequisitoModalidad? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPrerequisitoModalidad> ObtenerPorIdPGeneralPreRequisito(int idProgramaGeneralPrerequisito);
    }
}
