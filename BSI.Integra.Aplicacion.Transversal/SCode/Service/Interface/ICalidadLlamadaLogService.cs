using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICalidadLlamadaLogService
    {
        #region Metodos Base
        CalidadLlamadaLog Add(CalidadLlamadaLog entidad);
        CalidadLlamadaLog Update(CalidadLlamadaLog entidad);
        bool Delete(int id, string usuario);

        List<CalidadLlamadaLog> Add(List<CalidadLlamadaLog> listadoEntidad);
        List<CalidadLlamadaLog> Update(List<CalidadLlamadaLog> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CalidadLlamadaLogDTO> ObtenerCalidadLlamadaLog();
    }
}
