using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteEquipoComputoRepository : IGenericRepository<TCriterioEvaluacionProceso>
    {
        #region Metodos Base
        TCriterioEvaluacionProceso Add(PostulanteEquipoComputo entidad);
        TCriterioEvaluacionProceso Update(PostulanteEquipoComputo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<PostulanteEquipoComputo> listadoEntidad);
        IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<PostulanteEquipoComputo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteEquipoComputo? ObtenerPorId(int id);
    }
}
