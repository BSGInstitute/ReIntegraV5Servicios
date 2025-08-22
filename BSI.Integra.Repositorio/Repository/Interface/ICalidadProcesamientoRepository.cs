using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICalidadProcesamientoRepository : IGenericRepository<TCalidadProcesamiento>
    {
        #region Metodos Base
        TCalidadProcesamiento Add(CalidadProcesamiento entidad);
        TCalidadProcesamiento Update(CalidadProcesamiento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCalidadProcesamiento> Add(IEnumerable<CalidadProcesamiento> listadoEntidad);
        IEnumerable<TCalidadProcesamiento> Update(IEnumerable<CalidadProcesamiento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
