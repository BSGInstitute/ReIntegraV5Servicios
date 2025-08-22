using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEsquemaEvaluacionPgeneralDetalleRepository : IGenericRepository<TEsquemaEvaluacionPgeneralDetalle>
    {
        #region Metodos Base
        TEsquemaEvaluacionPgeneralDetalle Add(EsquemaEvaluacionPgeneralDetalle entidad);
        TEsquemaEvaluacionPgeneralDetalle Update(EsquemaEvaluacionPgeneralDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEsquemaEvaluacionPgeneralDetalle> Add(IEnumerable<EsquemaEvaluacionPgeneralDetalle> listadoEntidad);
        IEnumerable<TEsquemaEvaluacionPgeneralDetalle> Update(IEnumerable<EsquemaEvaluacionPgeneralDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        EsquemaEvaluacionPgeneralDetalle? ObtenerPorId(int id);
    }
}
