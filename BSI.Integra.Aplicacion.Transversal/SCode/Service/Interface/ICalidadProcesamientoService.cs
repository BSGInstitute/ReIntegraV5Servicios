using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICalidadProcesamientoService
    {
        #region Metodos Base
        CalidadProcesamiento Add(CalidadProcesamiento entidad);
        CalidadProcesamiento Update(CalidadProcesamiento entidad);
        bool Delete(int id, string usuario);

        List<CalidadProcesamiento> Add(List<CalidadProcesamiento> listadoEntidad);
        List<CalidadProcesamiento> Update(List<CalidadProcesamiento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
