using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO; 

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICriterioEvaluacionCategoriumRepository : IGenericRepository<TCriterioEvaluacionCategorium>
    {
        #region Metodos Base
        TCriterioEvaluacionCategorium Add(CriterioEvaluacionCategorium entidad);
        TCriterioEvaluacionCategorium Update(CriterioEvaluacionCategorium entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCriterioEvaluacionCategorium> Add(IEnumerable<CriterioEvaluacionCategorium> listadoEntidad);
        IEnumerable<TCriterioEvaluacionCategorium> Update(IEnumerable<CriterioEvaluacionCategorium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        CriterioEvaluacionCategorium ObtenerPorId(int id);
        IEnumerable<CriterioEvaluacionCategorium> Obtener();
    }
}
