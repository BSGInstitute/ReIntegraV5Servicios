using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralPrerequisitoRespuestaRepository : IGenericRepository<TProgramaGeneralPrerequisitoRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralPrerequisitoRespuestum Add(ProgramaGeneralPrerequisitoRespuesta entidad);
        TProgramaGeneralPrerequisitoRespuestum Update(ProgramaGeneralPrerequisitoRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPrerequisitoRespuestum> Add(IEnumerable<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralPrerequisitoRespuestum> Update(IEnumerable<ProgramaGeneralPrerequisitoRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralPrerequisitoRespuestaDTO> ObtenerProgramaGeneralPrerequisitoRespuesta();
        ProgramaGeneralPrerequisitoRespuestaDTO ObtenerPrerequisitoRespuesta(int idOportunidad, int idPrerequisito);
        ProgramaGeneralPrerequisitoRespuesta ObtenerPorIdOportunidadIdPrerequisito(int idOportunidad, int idPrerequisito);
    }
}