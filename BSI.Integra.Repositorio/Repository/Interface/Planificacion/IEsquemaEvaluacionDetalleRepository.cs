using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEsquemaEvaluacionDetalleRepository : IGenericRepository<TEsquemaEvaluacionDetalle>
    {
        #region Metodos Base
        TEsquemaEvaluacionDetalle Add(EsquemaEvaluacionDetalle entidad);
        TEsquemaEvaluacionDetalle Update(EsquemaEvaluacionDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEsquemaEvaluacionDetalle> Add(IEnumerable<EsquemaEvaluacionDetalle> listadoEntidad);
        IEnumerable<TEsquemaEvaluacionDetalle> Update(IEnumerable<EsquemaEvaluacionDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        EsquemaEvaluacionDetalle? ObtenerPorId(int id);
        IEnumerable<EsquemaEvaluacionDetalle> ObtenerPorIdEsquemaEvaluacion(int idEsquemaEvaluacion);




    }
   
}
