using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaDetalleSolucionRespuestaRepository : IGenericRepository<TProgramaGeneralProblemaDetalleSolucionRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralProblemaDetalleSolucionRespuestum Add(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad);
        TProgramaGeneralProblemaDetalleSolucionRespuestum Update(ProgramaGeneralProblemaDetalleSolucionRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaDetalleSolucionRespuestum> Add(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaDetalleSolucionRespuestum> Update(IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaDetalleSolucionRespuesta> ObtenerTodo();
        ProgramaGeneralProblemaDetalleSolucionRespuesta ObtenerPorIdOportunidadIdProblemaSolucion(int idOportunidad, int idProblemaSolucion);
    }
}