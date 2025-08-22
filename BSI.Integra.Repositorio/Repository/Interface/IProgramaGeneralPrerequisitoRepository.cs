using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralPrerequisitoRepository : IGenericRepository<TProgramaGeneralPrerequisito>
    {
        #region Metodos Base
        TProgramaGeneralPrerequisito Add(ProgramaGeneralPrerequisito entidad);
        TProgramaGeneralPrerequisito Update(ProgramaGeneralPrerequisito entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPrerequisito> Add(IEnumerable<ProgramaGeneralPrerequisito> listadoEntidad);
        IEnumerable<TProgramaGeneralPrerequisito> Update(IEnumerable<ProgramaGeneralPrerequisito> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPrerequisito? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerProgramaGeneralPrerequisitoEspecificoPorIdOportunidad(int idOportunidad);
        List<CompuestoPreRequisitoModalidadDTO> ObtenerPreRequisitosPorModalidades(int idPGeneral);
    }
}