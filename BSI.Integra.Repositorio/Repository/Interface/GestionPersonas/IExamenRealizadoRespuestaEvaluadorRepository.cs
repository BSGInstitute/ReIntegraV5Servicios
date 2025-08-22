using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenRealizadoRespuestaEvaluadorRepository : IGenericRepository<TExamenRealizadoRespuestaEvaluador>
    {
        #region Metodos Base
        TExamenRealizadoRespuestaEvaluador Add(ExamenRealizadoRespuestaEvaluador entidad);
        TExamenRealizadoRespuestaEvaluador Update(ExamenRealizadoRespuestaEvaluador entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TExamenRealizadoRespuestaEvaluador> Add(IEnumerable<ExamenRealizadoRespuestaEvaluador> listadoEntidad);
        IEnumerable<TExamenRealizadoRespuestaEvaluador> Update(IEnumerable<ExamenRealizadoRespuestaEvaluador> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ExamenRealizadoRespuestaEvaluador? ObtenerPorId(int id);
    }
}
