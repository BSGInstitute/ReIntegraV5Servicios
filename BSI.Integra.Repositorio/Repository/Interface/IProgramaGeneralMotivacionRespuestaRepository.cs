using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralMotivacionRespuestaRepository : IGenericRepository<TProgramaGeneralMotivacionRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralMotivacionRespuestum Add(ProgramaGeneralMotivacionRespuesta entidad);
        TProgramaGeneralMotivacionRespuestum Update(ProgramaGeneralMotivacionRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralMotivacionRespuestum> Add(IEnumerable<ProgramaGeneralMotivacionRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralMotivacionRespuestum> Update(IEnumerable<ProgramaGeneralMotivacionRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralMotivacionRespuestaDTO> ObtenerProgramaGeneralMotivacionRespuesta();
        ProgramaGeneralMotivacionRespuesta ObtenerPorIdOportunidadIdMotivacion(int idOportunidad, int idMotivacion);
    }
}